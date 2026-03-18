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
  service_idINT PRIMARY KEY,
  service_description VARCHAR(30),
  service_typeCHAR(1) CHECK (service_type IN ('A', 'C', 'S')),
  hourly_rate MONEY,
  sales_ytd MONEY
);

-- Create sales table
CREATE TABLE sales (
  sales_idINT PRIMARY KEY,
  sales_dateDATE,
  amountMONEY CHECK (amount >= 0),
  service_idINT FOREIGN KEY REFERENCES database_services(service_id)
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

-- Populate verification table
INSERT INTO verify VALUES('database_services', (SELECT COUNT(*) FROM database_services), 5);
INSERT INTO verify VALUES('sales', (SELECT COUNT(*) FROM sales), 15);

-- Print verification data
PRINT 'Verification';
SELECT table_name, actual, expected, expected - actual discrepancy FROM verify;
DROP TABLE verify;
GO

-- Alter master table to add new column
ALTER TABLE database_services ADD last_activity_date DATE NULL;
GO

-- Update database_services records
UPDATE database_services SET last_activity_date = '2019-07-25' WHERE service_id = 101;
UPDATE database_services SET last_activity_date = '2020-12-31' WHERE service_id = 102;
UPDATE database_services SET last_activity_date = '2020-06-15' WHERE service_id = 103;
UPDATE database_services SET last_activity_date = '2019-01-23' WHERE service_id = 104;
UPDATE database_services SET last_activity_date = '2020-03-16' WHERE service_id = 105;
GO

-- Insert a new record into master table
INSERT INTO database_services VALUES(106, 'Authentication', 'S', 99, 480, '2015-05-01');
GO

-- Create a stored procedure
-- if update = 1 delete record(s), if update = 0 show record(s) that would be deleted
CREATE PROCEDURE purge_database_services 
  @cut_off_date DATE;
  @update INT = 0;
AS BEGIN
  IF @update = 1
    DELETE FROM database_services 
    WHERE last_activity_date < @cut_off_date;
  ELSE
    PRINT 'Record(s) that would be deleted';
    SELECT * FROM database_services 
    WHERE last_activity_date < @cut_off_date;
END
GO

-- Verification
PRINT 'Verify procedure';

-- SELECT all rows and columns from the master table
PRINT 'Master Table Before Changes'
SELECT * FROM database_services;

--Execute procedure passing a date 3 years ago from today
PRINT 'After 1st Call To Procedure'
EXEC purge_database_services @cut_off_date = '2017-01-01';

-- SELECT all rows and columns from the master table
PRINT 'Master Table After 1st Call'
SELECT * FROM database_services;

-- Execute procedure passing a date 3 years ago from today and 1 for @Update
PRINT 'After 2nd Call To Procedure'
EXEC purge_database_services @cut_off_date = '2017-01-01', @update = 1;

-- SELECT all rows and columns from the master table
PRINT 'Master Table After 2nd Call'
SELECT * FROM database_services;
GO
