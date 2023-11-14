-- Setting NOCOUNT ON suppresses completion messages for each INSERT
SET NOCOUNT ON

-- Set date format to year, month, day
SET DATEFORMAT ymd;

-- Make the master database the current database
USE master

-- If database MyDB exists, drop it
IF EXISTS (SELECT * FROM sysdatabases WHERE name = 'MyDB')
  DROP DATABASE MyDB;
GO

-- Create the MyDB database
CREATE DATABASE MyDB;
GO

-- Make the MyDB database the current database
USE MyDB;

-- Create database_services table
CREATE TABLE database_services (
  service_id INT PRIMARY KEY,
  service_description VARCHAR(30),
  service_type CHAR(1) CHECK (service_type IN ('A', 'C', 'S')),
  hourly_rate MONEY,
  sales_ytd MONEY
);

-- Create sales table
CREATE TABLE sales (
  sales_id INT PRIMARY KEY,
  sales_date DATE,
  amount MONEY CHECK (amount >= 0),
  service_id INT FOREIGN KEY REFERENCES database_services(service_id)
);
GO

-- Insert database_services records
INSERT INTO database_services VALUES(101, 'Crashlytics','A', 89, 560);
INSERT INTO database_services VALUES(102, 'Cloud Storage','S', 107, 350);
INSERT INTO database_services VALUES(103, 'Realtime Database','C', 129, 425);
INSERT INTO database_services VALUES(104, 'Hosting','S', 100, 465);
INSERT INTO database_services VALUES(105, 'Performance Monitoring', 'A', 70, 525);

-- Insert sales records
INSERT INTO sales VALUES(1, '2020-8-7', 150, 101);
INSERT INTO sales VALUES(2, '2020-8-8', 120, 102);
INSERT INTO sales VALUES(3, '2020-8-11', 230, 104);
INSERT INTO sales VALUES(4, '2020-8-15', 300, 101);
INSERT INTO sales VALUES(5, '2020-8-2', 90, 103);
INSERT INTO sales VALUES(6, '2020-9-5', 110, 101);
INSERT INTO sales VALUES(7, '2020-9-10', 125, 105);
INSERT INTO sales VALUES(8, '2020-9-16', 220, 105);
INSERT INTO sales VALUES(9, '2020-9-23', 150, 102);
INSERT INTO sales VALUES(10, '2020-10-6', 85, 103);
INSERT INTO sales VALUES(11, '2020-10-14', 140, 104);
INSERT INTO sales VALUES(12, '2020-10-28', 95, 104);
INSERT INTO sales VALUES(13, '2020-11-10', 80, 102);
INSERT INTO sales VALUES(14, '2020-11-21', 180, 105);
INSERT INTO sales VALUES(15, '2020-11-30', 250, 103);
GO

-- Create an unique index on service_description on database_services table
CREATE UNIQUE INDEX IX_database_services_service_description
ON database_services (service_description ASC);
GO

-- Create a view for most expensive services
CREATE VIEW high_end_services AS SELECT
  SUBSTRING(service_description, 1, 15) AS descriptions, sales_ytd
FROM database_services WHERE hourly_rate > (
  SELECT AVG(hourly_rate) FROM database_services
);
GO

-- Verify inserts
CREATE TABLE verify (table_name varchar(30), actual INT, expected INT);
GO

-- Populate verification tables
INSERT INTO verify VALUES('database_services', (
  SELECT COUNT(*) FROM database_services), 5
);
INSERT INTO verify VALUES('sales', (
  SELECT COUNT(*) FROM sales), 15
);

-- Display verification data
PRINT 'Verification';
SELECT table_name, actual, expected, expected - actual discrepancy 
FROM verify;
DROP TABLE verify;
GO

-- Create an INSERT trigger for each row
CREATE OR ALTER TRIGGER trigger_insert ON sales FOR INSERT 
AS
DECLARE @service_id INT;
DECLARE @new_amount MONEY;
DECLARE MY_CURSOR CURSOR FOR
SELECT service_id, amount FROM inserted;

OPEN MY_CURSOR;
FETCH NEXT FROM MY_CURSOR INTO @service_id, @new_amount;

WHILE @@FETCH_STATUS = 0 BEGIN
  UPDATE database_services SET sales_ytd += @new_amount
  WHERE service_id = @service_id;
  FETCH NEXT FROM MY_CURSOR INTO @service_id, @new_amount;
END;

CLOSE MY_CURSOR;
DEALLOCATE MY_CURSOR;
GO

-- Create trigger DELETE for each row
CREATE OR ALTER TRIGGER trigger_delete ON sales FOR DELETE 
AS
DECLARE @service_id INT;
DECLARE @old_amount MONEY;
DECLARE MY_CURSOR CURSOR FOR
SELECT service_id, amount FROM deleted;

OPEN MY_CURSOR;
FETCH NEXT FROM MY_CURSOR INTO @service_id, @old_amount;

WHILE @@FETCH_STATUS = 0 BEGIN
  UPDATE database_services SET sales_ytd -= @old_amount
  WHERE service_id = @service_id;
  FETCH NEXT FROM MY_CURSOR INTO @service_id, @old_amount;
END;

CLOSE MY_CURSOR;
DEALLOCATE MY_CURSOR;
GO

-- Create an UPDATE trigger for each row
CREATE OR ALTER TRIGGER trigger_update ON sales FOR UPDATE 
AS
DECLARE @service_id INT;
DECLARE @new_amount MONEY;
DECLARE @old_amount MONEY;
DECLARE MY_CURSOR CURSOR FOR
SELECT i.service_id, i.amount, d.amount FROM inserted i
JOIN deleted d ON i.sales_id = d.sales_id;

OPEN MY_CURSOR;
FETCH NEXT FROM MY_CURSOR INTO @service_id, @new_amount, @old_amount;

WHILE @@FETCH_STATUS = 0 BEGIN
  UPDATE database_services SET sales_ytd += (@new_amount - @old_amount)
  WHERE service_id = @service_id;
  FETCH NEXT FROM MY_CURSOR INTO @service_id, @new_amount, @old_amount;
END;

CLOSE MY_CURSOR;
DEALLOCATE MY_CURSOR;
GO

-- Verification
PRINT 'Verify triggers';

-- Print master table data before insert
PRINT 'Master Table Before Changes';
SELECT * FROM database_services;

-- Insert new record into sales table (id = 16)
INSERT INTO sales VALUES(16, '2020-09-15', 100000, 103);
-- OR
-- Insert multiple (id = 16, 17)
INSERT INTO sales VALUES(16, '2020-09-15', 100000, 103), (17, '2020-11-20', 150000, 102);

-- Print master table data after insert
PRINT 'After INSERT';
SELECT * FROM database_services;

-- Delete last inserted row from sales table (id = 16)
DELETE FROM sales WHERE sales_id = 16;
-- OR
-- Delete Multiple (id = 16, 17)
DELETE FROM sales WHERE sales_id IN (16, 17);

-- Print master table data after delete
PRINT 'After DELETE';
SELECT * FROM database_services;

-- Update a row (amount) in sales table (id = 15)
UPDATE sales SET amount += 200000 WHERE sales_id = 15;
-- OR
-- Update multiple (id = 14, 15)
UPDATE sales SET amount += 200000 WHERE sales_id IN (14, 15);

-- Print master table data after update
PRINT 'After UPDATE';
SELECT * FROM database_services;
GO
