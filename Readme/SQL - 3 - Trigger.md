# SQL - Triggers

A trigger is a special stored procedure attached to a specific table. Managed by DBMS.

## Triggers Versus Stored Procedures

| Triggers                                   | Stored Procedures                                |
| :----------------------------------------- | :----------------------------------------------- |
| Attached to a specific table               | Not attached to a specific table                 |
| Can’t pass values to a trigger             | Can pass values to a procedure                   |
| “Fired” automatically in response to event | Must be called by programs, triggers or manually |
| Event-driven                               | Not event-driven                                 |
| e.g. `INSERT`, `UPDATE`, `DELETE`          | Can be scheduled                                 |

## Why use Triggers?

- Enforce business rules too complex for CHECK constraints or Referential Integrity
- Automate activity
- Access or modify other tables
- Downside: will slow down triggering operation (`INSERT`, `UPDATE` or `DELETE`)

## Use a Trigger to enforce a business rule

- **Business rule #1**:
- No more than three suppliers are permitted to supply any single part
- A trigger can check how many rows already exist for a specific part and prevent an `INSERT`
- **Business rule #2**:
- The lowest cost supplier will always be used
- A trigger can disallow any order that does not use the lowest quote for a part

## Use a Trigger to perform calculations

- Use a trigger to calculate the order amount for the part based on the supplier chosen to provide the parts

## Trigger syntax

```sql
CREATE TRIGGER Trigger_Name ON Table_Name
AFTER [INSERT] [,] [UPDATE] [,] [DELETE]
AS
  -- Optionally declare variables here
BEGIN
  -- Insert statements for trigger here
END
```

### Example

```sql
CREATE TRIGGER employees_insert ON employees
AFTER INSERT AS
UPDATE company_stats SET num_of_emp = num_of_emp + 1;

-- Test
PRINT '*Before Insert*'
SELECT * FROM employees;
SELECT * FROM company_stats;

PRINT '*Insert*'
INSERT INTO employees (employee_id, first_name, last_name)
VALUES(110, 'Bob', 'Loblaw');

PRINT '*After Insert*'
SELECT * FROM employees;
SELECT * FROM company_stats;
```

## Deleted and Inserted

- During trigger execution, two special tables are used: deleted and inserted
- SQL Server automatically creates and manages these tables
- They have identical schema to the table being modified

| Trigger Event | deleted    | inserted   |
| :------------ | :--------- | :--------- |
| DELETE        | Old row(s) | Not used   |
| INSERT        | Not used   | New row(s) |
| UPDATE        | Old row(s) | New row(s) |

### Trigger 1

```sql
CREATE TRIGGER employees_update ON employees
-- Trigger will run AFTER modification occurs
AFTER UPDATE AS
  DECLARE @new_salary MONEY;
  DECLARE @old_salary MONEY;
BEGIN
  SELECT @new_salary = (
    -- `inserted` used for values before the update
    SELECT salary FROM inserted
  );

  SELECT @old_salary = (
    -- `deleted` used for values after the update
    SELECT salary FROM deleted
  );

  IF @new_salary > (@old_salary * 1.5)
    ROLLBACK TRANSACTION; -- Cancel the UPDATE
END

-- Test
PRINT '*Before Update*'
SELECT * FROM employees WHERE employee_id = 102;

PRINT '*Attempted Update*'
UPDATE employees SET salary = 1500 WHERE employee_id = 102;
GO

PRINT '*After Attempted Update*'
SELECT * FROM employees WHERE employee_id = 102;

PRINT '*Successful Update*'
UPDATE employees SET salary = 850 WHERE employee_id = 102;
GO

PRINT '*After Successful Update*'
SELECT * FROM employees WHERE employee_id = 102;

-- employees_update Trigger
-- If multiple rows are updated employees_update will fail
SELECT @new_salary = (SELECT salary FROM inserted);

-- Test
PRINT '*Before Update*'
SELECT * FROM employees WHERE department = 'Sales';

PRINT '*Attempted Multiple Row Update*'
UPDATE employees SET salary = 700 WHERE department = 'Sales';
GO

PRINT '*After Update*'
SELECT * FROM employees WHERE department = 'Sales';
```

### Trigger 2

```sql
ALTER TRIGGER employees_update ON employees
AFTER UPDATE AS
BEGIN
  IF EXISTS (
    SELECT i.salary FROM inserted i
    JOIN deleted d ON i.employee_id = d.employee_id
    WHERE i.salary > (d.salary * 1.5)
  ) ROLLBACK TRANSACTION;
END

-- Test
PRINT '*Before Update*'
SELECT * FROM employees WHERE department = 'Sales';

PRINT '*Successful Multiple Row Update*'
UPDATE employees SET salary = 600 WHERE department = 'Sales';
GO

PRINT '*Attempted Single Row Update of 102*'
UPDATE employees SET salary = 1500 WHERE employee_id = 102;
GO

PRINT '*After Update*'
SELECT * FROM employees WHERE department = 'Sales';
```

### Trigger 3

```sql
ALTER TRIGGER employees_update ON employees
AFTER UPDATE AS
  DECLARE @new_salary MONEY;
  DECLARE @old_salary MONEY;
  DECLARE employees_cursor CURSOR
  FOR SELECT i.salary, d.salary FROM inserted i
  JOIN deleted d ON i.employee_id = d.employee_id;
BEGIN
  OPEN employees_cursor;
  FETCH NEXT FROM employees_cursor
  INTO @new_salary, @old_salary;
  WHILE @@FETCH_STATUS = 0 BEGIN
    IF @new_salary > @old_salary * 1.5 BEGIN
      ROLLBACK TRANSACTION;
      BREAK;
    END
    FETCH NEXT FROM employees_cursor INTO @new_salary, @old_salary;
  END
  CLOSE employees_cursor;
END

-- Test
PRINT '*Before Update*'
SELECT * FROM employees WHERE department = 'Sales';

PRINT '*Successful Multiple Row Update*'
UPDATE employees SET salary = 750 WHERE department = 'Sales';
GO

PRINT '*Attempted Single Row Update of 102*'
UPDATE employees SET salary = 1500 WHERE employee_id = 102;
GO

PRINT '*After Update*'
SELECT * FROM employees WHERE department = 'Sales';
```

## Firing Triggers

- Timing choices
- `AFTER`: after firing activity occurs
- `INSTEAD OF`: allows DBA to take complete control of modification
- Oracle, MySQL and DB2 also support `BEFORE` triggers
- `AFTER` example
- **Insert**, **update** or **delete** occurs first
- Trigger logic is executed
- Trigger can **roll back** data modification

```sql
-- PurchaseOrderLineInsert
CREATE TRIGGER purchase_order_lines_insert ON purchase_order_lines
AFTER INSERT AS
UPDATE purchase_orders SET total_amount = total_amount + (
  SELECT SUM(quantity * unit_cost) FROM inserted
  WHERE purchase_orders.purchase_order_id = inserted.purchase_order_id
)
WHERE purchase_orders.purchase_order_id IN (
  SELECT purchase_order_id FROM inserted
);

-- Test
PRINT '*Before Insert*'
SELECT po.purchase_order_id, total_amount, SUM(quantity * unit_cost) AS Actual
FROM purchase_orders po
JOIN purchase_order_lines pol
ON po.purchase_order_id = pol.purchase_order_id
WHERE po.purchase_order_id = 50
GROUP BY po.purchase_order_id, total_amount;

PRINT '*Insert*'
INSERT INTO purchase_order_lines
VALUES(50, 2, 20, 10, 24.89, 0, 0, NULL);

PRINT '*After Insert*'
SELECT po.purchase_order_id, total_amount, SUM(quantity * unit_cost) AS Actual
FROM purchase_orders po
JOIN purchase_order_lines pol
ON po.purchase_order_id = pol.purchase_order_id
WHERE po.purchase_order_id = 50
GROUP BY po.purchase_order_id, total_amount;
```

## One Trigger causes need for others

- If an `INSERT` trigger is keeping the master table updated when new records are inserted into a child table
- What happens on `UPDATE`s or `DELETE`s to the child table?
- Additional triggers are required to keep the tables in sync

```sql
-- purchase_order_lines_update
CREATE TRIGGER purchase_order_lines_update ON purchase_order_lines
AFTER UPDATE AS
UPDATE purchase_orders SET total_amount = total_amount + (
  SELECT SUM(quantity * unit_cost) FROM inserted
  WHERE purchase_orders.purchase_order_id = inserted.purchase_order_id
) - (
  SELECT SUM(quantity * unit_cost) FROM deleted
  WHERE purchase_orders.purchase_order_id = deleted.purchase_order_id
)
WHERE purchase_orders.purchase_order_id IN (
  SELECT purchase_order_id FROM inserted
);

-- Test
PRINT '*Before Update*'
SELECT po.purchase_order_id, total_amount, SUM(quantity * unit_cost) AS Actual
FROM purchase_orders po
JOIN purchase_order_lines pol
ON po.purchase_order_id = pol.purchase_order_id
WHERE po.purchase_order_id = 50
GROUP BY po.purchase_order_id, total_amount;

UPDATE purchase_order_lines SET quantity = 1
WHERE purchase_order_id = 50 AND line_num = 2;

PRINT '*After Update*'
SELECT po.purchase_order_id, total_amount, SUM(quantity * unit_cost) AS Actual
FROM purchase_orders po
JOIN purchase_order_lines pol
ON po.purchase_order_id = pol.purchase_order_id
WHERE po.purchase_order_id = 50
GROUP BY po.purchase_order_id, total_amount;
```

### Sample Trigger

```sql
-- purchase_order_lines_delete
CREATE TRIGGER purchase_order_lines_delete ON purchase_order_lines
AFTER DELETE
AS
UPDATE purchase_orders SET total_amount = total_amount - (
  SELECT SUM(quantity * unit_cost) FROM deleted
  WHERE purchase_orders.purchase_order_id = deleted.purchase_order_id
)
WHERE purchase_orders.purchase_order_id IN (
  SELECT purchase_order_id FROM deleted
);

-- Test
PRINT '*Before Delete*'
SELECT po.purchase_order_id, total_amount, SUM(quantity * unit_cost) AS Actual
FROM purchase_orders po
JOIN purchase_order_lines pol
ON po.purchase_order_id = pol.purchase_order_id
WHERE po.purchase_order_id = 50
GROUP BY po.purchase_order_id, total_amount;

PRINT '*Delete*'
DELETE FROM purchase_order_lines
WHERE purchase_order_id = 50 AND line_num = 2

PRINT '*After Delete*'
SELECT po.purchase_order_id, total_amount, SUM(quantity * unit_cost)
AS Actual
FROM purchase_orders po
JOIN purchase_order_lines pol
ON po.purchase_order_id = pol.purchase_order_id
WHERE po.purchase_order_id = 50
GROUP BY po.purchase_order_id, total_amount;
```

## Deleting a Trigger

```sql
DROP TRIGGER Trigger_Name;
```

## Data Dictionary Tables For Triggers

- Every `CREATE TRIGGER` statement generates entries in
- `sys.triggers`
- `sys.trigger_events`
- `sys.events`
- `sys.sql_modules`
