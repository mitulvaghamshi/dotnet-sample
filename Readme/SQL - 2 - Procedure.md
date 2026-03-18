# SQL - Stored Procedures

- Pieces of executable code stored in the database (small programs)
- Best practice is for programming languages to call stored procedures for database access
- Compiled, therefore faster
- Reduces network traffic
- More secure
- Consistent
- DBA approved

## Body Statements

- SQL is non-procedural and set oriented
- Stored procedures require procedural code
- Each database vendor extends SQL with procedural code
- Microsoft uses T-SQL
- Oracle uses Procedural Language SQL (PL/SQL)
- IBM uses SQL Procedural Language (SQL PL)
- MySQL uses SQL/Persistent Stored Module (SQL/PSM)

## T-SQL Keywords

| BEGIN/END | BREAK     | CONTINUE |
| :-------- | :-------- | :------- |
| GOTO      | IF/ELSE   | RETURN   |
| TRY/CATCH | WAITFOR   | WHILE    |
| CASE      | DECLARE   | EXECUTE  |
| FETCH     | RAISERROR | ROLLBACK |
| SET       |           |          |

## Stored Procedures execution

- Invoked by `EXECUTE` (`EXEC`) statement in T-SQL. `EXEC` is optional, allows autocomplete to help
- Can accept command line parameters, separated by commas, parameters can be positional or named

## System Stored Procedures

- SQL Server provides many stored procedures
- Many are used for database administration
- Typing the name of a system stored procedure in a query window and pressing F1 will get help on the procedure
- Right clicking the procedure in the Object Explorer and selecting Modify will reveal the procedure code
- sp_columns, sp_help, sp_server_info, etc...

```sql
EXEC sp_columns 'patients'
EXEC sp_columns @table_name='patients'
EXEC sp_help
sp_help 'patients'
sp_server_info
```

## Stored Procedure Structure

- A stored procedure consists of:
- Procedure name
- Set of optional parameters
- Routine body

```sql
-- Increase and vendor are hard coded, not very flexible
CREATE PROCEDURE update_items_item_cost AS
UPDATE items SET item_cost = item_cost * (1 + 0.25)
WHERE primary_vendor_id = 1;

-- Test
PRINT '*Before Update*'
SELECT * FROM items ORDER BY primary_vendor_id;

EXEC update_items_item_cost;

PRINT '*After Update*'
SELECT * FROM items ORDER BY primary_vendor_id;
```

## ALTER SQL Statement

- `ALTER PROCEDURE` allows changing an existing stored procedure
- `ALTER TABLE` allows for changing table structure
- Adding/Removing/Changing of columns, constraints, etc...
- `ALTER DATABASE`, `ALTER VIEW`, etc...

```sql
-- Increase and vendor are parameters, improved flexibility
ALTER PROCEDURE update_items_item_cost
@increase DECIMAL(3, 2), @vendor_id INT AS
UPDATE items SET item_cost = item_cost * (1 + @increase)
WHERE primary_vendor_id = @vendor_id;

-- Test
PRINT '*Before Update*'
SELECT * FROM items WHERE primary_vendor_id = 1;

EXEC update_items_item_cost 0.25, 1;

PRINT '*After 1st Update*'
SELECT * FROM items WHERE primary_vendor_id = 1;

EXEC update_items_item_cost @increase=0.25, @vendor_id=1;

PRINT '*After 2nd Update*'
SELECT * FROM items WHERE primary_vendor_id = 1;
```

```sql
-- 3rd parameter allows conditional update, defaults to no update
ALTER PROCEDURE update_items_item_cost
  @increase DECIMAL(3, 2),
  @vendor_id INT,
  @update INT = 0 -- If 1, perform update, else show what would be updated
AS BEGIN
  IF @update = 1
    UPDATE items SET item_cost = item_cost * (1 + @increase)
    WHERE primary_vendor_id = @vendor_id;
  ELSE
    SELECT item_id, vendor_name, item_cost AS [existing_cost],
      item_cost * (1 + @increase) AS [proposed_cost]
    FROM items
    JOIN vendors ON primary_vendor_id = vendors.vendor_id
    WHERE primary_vendor_id = @vendor_id;
END

-- Test
PRINT '*Before Update*'
SELECT * FROM items WHERE primary_vendor_id = 1;

PRINT '*No Update*'
EXEC update_items_item_cost @increase=0.25, @vendor_id=1;

PRINT '*After No Update*'
SELECT * FROM items WHERE primary_vendor_id = 1;

PRINT '*Update*'
EXEC update_items_item_cost @increase=0.25, @vendor_id=1, @update=1;

PRINT '*After Update*'
SELECT * FROM items WHERE primary_vendor_id = 1;
```

```sql
-- 4th parameter will be returned to caller with number of rows affected
-- This version also makes use of a database cursor (more later)
ALTER PROCEDURE update_items_item_cost
  @increase DECIMAL(3, 2),
  @vendor_id INT,
  @update INT = 0, -- If 1, perform update, else show what would be updated
  @number_of_records INT OUTPUT
AS
  DECLARE items_cursor CURSOR
  FOR SELECT item_cost FROM items
  WHERE primary_vendor_id = @vendor_id
  FOR UPDATE;

  DECLARE @item_cost DECIMAL(9, 2);

  BEGIN
    IF @update = 1 BEGIN
      SET @number_of_records = 0;
      OPEN items_cursor;
      FETCH NEXT FROM items_cursor INTO @item_cost;
      WHILE @@FETCH_STATUS = 0 BEGIN
        SET @item_cost = @item_cost * (1 + @increase);
        UPDATE items SET item_cost = @item_cost
        WHERE CURRENT OF items_cursor;
        SET @number_of_records = @number_of_records + 1;
        FETCH NEXT FROM items_cursor INTO @item_cost;
      END
      CLOSE items_cursor;
    END
    ELSE -- No Update
      SELECT item_id, vendor_name, item_cost AS [existing_cost],
      item_cost * (1 + @increase) AS [proposed_cost]
      FROM items
      JOIN vendors ON primary_vendor_id = vendors.vendor_id
      WHERE primary_vendor_id = @vendor_id;
    DEALLOCATE items_cursor;
  END

-- Test
PRINT '*Before Update*'
SELECT * FROM items WHERE primary_vendor_id = 1;

-- Need to declare a variable to receive output
DECLARE @num_of_rows INT;

PRINT '*No Update*'
EXEC update_items_item_cost @increase=0.25, @vendor_id=1, @update=0, @number_of_records=@num_of_rows OUTPUT;
SELECT @num_of_rows AS [number_of_rows];

PRINT '*After No Update*'
SELECT * FROM items WHERE primary_vendor_id = 1;

PRINT '*Update*'
EXEC update_items_item_cost @increase=0.25, @vendor_id=1, @update=1, @number_of_records=@num_of_rows OUTPUT;
SELECT @num_of_rows AS [number_of_rows];

PRINT '*After Update*'
SELECT * FROM items WHERE primary_vendor_id = 1;
```

## Variable Declarations

- Declare local variables used for
- Calculations
- Assignment to output parameters
- Assignment to columns for database updates
- As input parameters passed by calling programs
- Error handling

```sql
DECLARE @total_sales DECIMAL(11,2);
DECLARE @number_customers INT;
DECLARE @error_msg CHAR(10);
DECLARE @order_date DATETIME;
DECLARE @picture VARBINARY(MAX);
```

## Assignment Statement SET

- Used to assign values to
- Input and output parameters
- Local variables
- Conform to SQL arithmetic operators
- Compatible data types of target and source

```sql
SET @record_count = @record_count + 1;
SET @credit_limit = @credit_limit * 1.20;
SET @num_orders = NULL;
SET @max_credit_limit = (SELECT MAX(credit_limit) FROM customers);
```

### IF

- Tests a simple condition
- If the condition evaluates to TRUE, the next line of code is executed
- If the condition evaluates to FALSE, the control of the program is passed to the next statement after the test

```sql
IF @ref_error = 1
SET @error_msg = 'NOT FOUND';
```

### IF-ELSE

- Similar to the IF structure
- The difference is that when the condition evaluates to FALSE, the statement following the ELSE keyword is executed

```sql
IF @ref_error = 0
SET @error_msg = 'FOUND';
ELSE
SET @error_msg = 'NOT FOUND';
```

### BEGIN-END

- Used to enclose a block of statements where a single statement can be used

```sql
IF @ref_error = 0 BEGIN
  SET @error_msg = 'FOUND';
  SET @found = @found + 1;
END ELSE BEGIN
  SET @error_msg = 'NOT FOUND';
  SET @not_found = @not_found + 1;
END
```

### Nested IFs

```sql
IF @evaluation = 100
  SET @new_salary = salary * 1.3;
ELSE BEGIN
  IF @evaluation >= 90
    SET @new_salary = salary * 1.2;
  ELSE
    SET @new_salary = salary * 1.1;
END
```

### CASE

- Permits you to select an execution path based on multiple cases
- Two options for coding a CASE structure

```sql
-- Using the first option
CASE @evaluation
WHEN 100 THEN
    UPDATE employees SET salary = salary * 1.3;
WHEN 90 THEN
  UPDATE employees SET salary = salary * 1.2;
WHEN 80 THEN
  UPDATE employees SET salary = salary * 1.1;
ELSE
  UPDATE employees SET salary = salary * 1.05;
END
```

```sql
-- Using the second option
CASE
WHEN @evaluation = 100 THEN
  UPDATE employees SET salary = salary * 1.3;
WHEN @evaluation = 90 THEN
  UPDATE employees SET salary = salary * 1.2;
WHEN @evaluation = 80 THEN
  UPDATE employees SET salary = salary * 1.1;
ELSE
  UPDATE employees SET salary = salary * 1.05;
END
```

### WHILE

- Loop while the condition is true
- Used with BEGIN-END
- Frequently used with cursors

```sql
WHILE @@FETCH_STATUS = 0 BEGIN
  -- loop processing
END
```

### BREAK

- Used to exit a WHILE loop

```sql
WHILE @@FETCH_STATUS = 0 BEGIN
  -- loop processing
  IF @count = 5
    BREAK;
END
```

### CONTINUE

- Used to advance to the next iteration of a WHILE loop

```sql
WHILE @@FETCH_STATUS = 0 BEGIN
  -- loop processing
  IF @price = 5.0
    CONTINUE;
  -- loop processing
END
```

## Deleting A Procedure

- To delete an existing procedure use:

```sql
DROP PROCEDURE procedure_name;
```

## Data Dictionary Tables For Procedures

- Every `CREATE PROCEDURE` statement generates entries in
- `sys.procedures`
- `sys.sql_modules`

## Cursors

- Used to access a `SELECT` result set one row at a time
- Steps to use a cursor
- `DECLARE` a cursor with a `SELECT` statement
- `OPEN` the cursor
- This executes the `SELECT` statement and populates the cursor
- `FETCH` one row at a time from the result set `INTO` variables
- Each column fetched must have a correlating variable
- Perform whatever processing desired for each row
- `CLOSE` the cursor
- `DEALLOCATE` the cursor

### @@FETCH_STATUS

- Returns the status of the last cursor `FETCH` statement
- 0: The `FETCH` statement was successful
- -1: The `FETCH` statement failed or the row was beyond the result set
- -2: The row fetched is missing
