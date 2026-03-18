# SQL - Concurrency Control

- Transactions
- Locks
- Concurrent processing

## Scenario

- You're about to buy a new hoodie at the Campus Store
- Here is what three records in the database might look like before you make your purchase at the checkout counter
- How many database operations are involved in this case?

| student_id | name  |
| :--------- | :---- |
| balance    | 12345 |
| Stu Dent   | 95.00 |

| prod_code  | Item         |
| :--------- | :----------- |
| unit_price | on_hand      |
| H1200      | Black Hoodie |
| 40.00      | 9            |

| account_no | description |
| :--------- | :---------- |
| balance    | 101         |
| Cash       | 750.00      |

### SQL For Scenario (Simplified)

```sql
UPDATE one_card
SET balance = balance – 40
WHERE student_id = 12345;

UPDATE inventory
SET on_hand = on_hand – 1
WHERE prod_code = 'H1200';

UPDATE account
SET balance = balance + 40
WHERE account_no = 101;
```

### Scenario (Realistic)

```sql
CREATE PROCEDURE purchase
  @student_id INT,
  @qty INT,
  @prod_code VARCHAR(25),
  @return_code INT = 0 OUTPUT -- Assume transaction doesn't succeed
AS
  DECLARE @available MONEY;
  DECLARE @extended_price MONEY;
BEGIN
  SET @available = (
    SELECT balance FROM one_card
    WHERE student_id = @student_id
  );

  SET @extended_price = @qty * (
    SELECT unit_price FROM inventory
    WHERE prod_code = @prod_code
  );

  IF @available >= @extended_price BEGIN
    UPDATE one_card
    SET balance = balance – @extended_price
    WHERE student_id = @student_id;

    UPDATE inventory
    SET on_hand = on_hand – @qty
    WHERE prod_code = @prod_code;

    UPDATE account
    SET balance = balance + @extended_price
    -- This value likely wouldn't be hardcoded.
    WHERE account_no = 101;

    SET @return_code = 1;
  END
END
```

### Scenario

- After the transaction, the rows will look like this

| student_id | name  |
| :--------- | :---- |
| balance    | 12345 |
| Stu Dent   | 55.00 |

| prod_code  | item         |
| :--------- | :----------- |
| unit_price | on_hand      |
| H1200      | Black Hoodie |
| 40.00      | 8            |

| account_no | description |
| :--------- | :---------- |
| balance    | 101         |
| Cash       | 790.00      |

## Transactions

- Definition: A logical unit of work that must be completed in its entirety or be aborted
- A transaction is created every time you read from and/or write to a database
- Many transactions consist of two or more database requests
- A transaction that changes the contents of the database must alter the database from one consistent state to another
- A consistent database state is one in which all integrity constraints are satisfied

### Transaction Properties

- All transactions must adhere to the ACID rules
- Atomicity
- Consistency
- Isolation
- Durability

#### Atomicity

- Either the effects of all or none of its operations remain when a transaction is completed (committed or aborted respectively)
- In other words, to the outside world a committed transaction appears to be indivisible, atomic
- A transaction is a unit of work that appears as if it is either performed in its entirety, or not performed at all ("all or nothing" semantics)
- How does this property apply to our scenario?
- One Card, Inventory and Account must be successfully updated, otherwise changes are not made

#### Consistency

- Every transaction must leave the database in a consistent state, i.e., maintain the predetermined integrity rules of the database
- All constraints (Entity, Domain and Referential Integrity) must be satisfied with each update
- A transaction must transform a database from one consistent state to another consistent state

#### Isolation

- Transactions cannot interfere with each other
- The effects of an incomplete transaction are not visible to another transaction
- A lock guarantees exclusive use of a data item to a current transaction
- Providing isolation is the main goal of concurrency control

#### Durability

- Effects of successful (committed) transactions must persist through crashes or system failure
- Typically by recording the transaction's effects and its commit event in a non-volatile memory (disk)

### Transaction Management

- ANSI standard to govern database transactions supported by SQL commands `BEGIN TRANSACTION`, `COMMIT` and `ROLLBACK`
- `BEGIN TRANSACTION` starts the transaction
- `COMMIT` completes the transaction
- `ROLLBACK` aborts all changes, returning the database to its previous consistent state
- Transaction logs are used to keep track of all transactions that update the database in case of `ROLLBACK`, a bend or system failure

### ROLLBACK

```sql
SELECT * FROM physicians WHERE physician_id = 2;
SELECT * FROM patients WHERE patient_id = 1251;

BEGIN TRANSACTION;

UPDATE physicians SET specialty = 'Hematology' WHERE physician_id = 2;
UPDATE patients SET allergies = 'Almonds' WHERE patient_id = 1251;

SELECT * FROM physicians WHERE physician_id = 2;
SELECT * FROM patients WHERE patient_id = 1251;

ROLLBACK TRANSACTION;

SELECT * FROM physicians WHERE physician_id = 2;
SELECT * FROM patients WHERE patient_id = 1251;
```

### COMMIT

```sql
SELECT * FROM physicians WHERE physician_id = 2;
SELECT * FROM patients WHERE patient_id = 1251;

BEGIN TRANSACTION T1; -- Optional transaction name

UPDATE physicians SET specialty = 'Hematology' WHERE physician_id = 2;
UPDATE patients SET allergies = 'Almonds' WHERE patient_id = 1251;

SELECT * FROM physicians WHERE physician_id = 2;
SELECT * FROM patients WHERE patient_id = 1251;

COMMIT TRANSACTION T1;

SELECT * FROM physicians WHERE physician_id = 2;
SELECT * FROM patients WHERE patient_id = 1251;
```

## Concurrent Processing

- The co-ordination of simultaneous execution of transactions in a multi-processing database system
- To ensure serialize-ability of transactions
- Execution of transactions in the correct sequence

## Concurrency Control Use Cases

- Need to avoid data integrity and consistency problems
- Lost update
- One record simultaneously updated by two different users
- Uncommitted data
- Two concurrent transactions, the first is rolled back after the second has accessed the uncommitted data, violating isolation property
- Inconsistent retrievals
- One transaction is executing calculations on a set of data while a second transaction is updating the same data

## Locks

- Guarantee exclusive use of a data item to a current transaction
- Released when transaction is committed
- Managed by a lock manager
- Lock levels:
- Database
- Table
- Page
- Row
- Field

## Two Phase Locking Protocol (2PL)

- Defines how transactions acquire and release locks
- Guarantees serialize-ability
- Does not prevent deadlocks
- Two phases
- Growing phase: a transaction acquires all required locks, without unlocking any data, until lock point
- Shrinking phase: a transaction releases all locks and cannot obtain any new lock

![][two-phase-lock]

## 2PL Protocol

- Rules
- Two transactions cannot have conflicting locks
- No unlock operation can precede a lock operation in the same transaction
- No data are affected until all locks have been obtained

## Lock Modes

- Shared lock
- One user asks to read a record
- Other users may read it
- No user can update it
- Update lock
- Temporary state before Exclusive lock
- Exclusive lock
- One user asks to modify a record
- No other user can read it or modify it
- Which SQL statement results in a shared lock?
- Why bother locking a record being read?
- Why is an exclusive lock necessary?
- Why prevent users from reading the record?

## Deadlock

- Occurs when two transactions wait for each other to unlock data
- T1 obtains a lock on X
- T2 obtains a lock on Y
- T1 requests a lock on Y
- T2 requests a lock on X
- Only exists with exclusive locks

## Deadlock Control

- Prevention
- Transaction is aborted and rescheduled if possibility of deadlock exists
- Detection
- If a deadlock is found, one transaction is aborted (rolled back) and rescheduled while the other continues
- Avoidance
- Obtain all locks, in succession, before executing
- increase action response times
- Recommendations:
- Low probability: detection
- High probability: prevention
- Use avoidance where response time is a low priority

## Database Recovery Management

- The database failing for unexpected reasons occurs occasionally
- Possible causes are power or hardware failure
- Recovery restores the database from a given state, usually inconsistent, to a previously consistent state
- Techniques used based on atomic transaction property

## Recovery in SQL Server

- As each modification to the database, it goes to the transaction log first, it will be written to the database at a later time
- Log records for data modifications record either the logical operation performed or they record the before and after images of the modified data
- The before image is a copy of the data before the operation is performed; the after image is a copy of the data after the operation has been performed
- In the event of a crash, the Database Administrator restores the database from the latest backup and then applies the transaction log

[two-phase-lock]: <data:image/webp;base64,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>
