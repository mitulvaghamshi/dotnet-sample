/*****************************************************************
 * Script: 		CalculateNetSalary.sql
 * Author: 		Mitul Vaghamshi
 * Date: 		Mar 29, 2023
 * Description: Calculate net salary by deducting all taxes,
 * 				insurances, contributions and applying benefits.
 *
 * Basic operation:
 *  1. Take gross salary from user.
 *  2. Apply deductions as EI, CPP and IncomeTex as percentages.
 *  3. Add Bonus and Allowance in dollars from users.
 *  4. Apply deduction of 1% IncomeTex for female only.
 *  5. Apply conditions on dependants as:
 *     - 2 dependants no deductions in IncomeTex.
 *     - 3 dependants deduction of 2.
 *     - 4 dependants deduction of 4.
 *
 * References:
 *  - [wealthsimple.com](https://www.wealthsimple.com/en-ca/tool/tax-calculator/ontario)
 *  - [filingtaxes.ca](https://filingtaxes.ca/why-are-bonuses-taxed-so-high-in-ontario)
 *  - [canada.ca - ei](https://www.canada.ca/en/revenue-agency/services/tax/businesses/topics/payroll/payroll-deductions-contributions/employment-insurance-ei/ei-premium-rates-maximums.html)
 *  - [canada.ca - cpp](https://www.canada.ca/en/revenue-agency/services/tax/businesses/topics/payroll/payroll-deductions-contributions/canada-pension-plan-cpp/checking-amount-cpp-you-deducted.html)
*****************************************************************/

-- SAMPLE OUTPUT
/*********************************************************************************************************************
EMPLOYEE NAME	GENDER		DEPENDANTS	GROSS SALARY + ADDITIONS	TOTAL TAX	CPP			EI			NET SALARY
Karry			Male      	4			81325.00					22415.0162	6114.35		971.70		61266.3832
Sujawal			Female    	1			77650.00					21325.3787	6114.35		971.70		56887.8675
Allana			Female    	3			120280.00					37326.3393	6114.35		971.70		85458.8612
John			Male      	2			55180.00					13646.502	5083.764	871.844		41533.498
Sally			Female    	3			58250.00					14807.0199	5382.475	920.35		44754.9581
Jane			Male      	4			79150.00					21770.1287	6114.35		971.70		59675.0662
Jiggar			Male      	3			78160.00					21476.5937	6114.35		971.70		57817.0744
Nattaly Hill	Female    	3			78680.00					21630.7737	6114.35		971.70		58772.113
*********************************************************************************************************************/

-- SAMPLE DATA
/***************************************************************************************************************************************
emp_id      emp_name     emp_email              emp_password  emp_gender      noOfDependants Additions  ITex   CPP    EI    GrossSalary
----------- ------------ ---------------------- ------------- --------------- -------------- ---------- ------ ------ ----- -----------
2           Karry        KKhall@yahoo.com       NULL          Male            4              325.00     0.21   0.07   0.05  81000.00
1148        Sujawal      sujawalfff@yahoo.com   NULL          Female          1              650.00     0.25   0.04   0.03  77000.00
2200        Allana       Allana@gmail.com       NULL          Female          3              280.00     0.28   0.05   0.04  120000.00
2204        John         jadde@yahoo.com        NULL          Male            2              180.00     0.28   0.24   0.04  55000.00
2205        Sally        salley@yahoo.com       NULL          Female          3              250.00     0.28   0.04   0.05  58000.00
2207        Jane         jane@yahoo.com         NULL          Male            4              150.00     0.05   0.04   0.04  79000.00
2209        Jiggar       jghdd25@yahoo.com      NULL          Male            3              160.00     0.25   0.05   0.04  78000.00
2212        Nattaly Hill natilli@yahoo.com      NULL          Female          3              180.00     0.28   0.05   0.03  78500.00
***************************************************************************************************************************************/

-- REMOVE {SALARY_DB} DATABASE IF ALREADY EXISTS
IF EXISTS (SELECT * FROM sysdatabases WHERE name = 'SALARY_DB')
  DROP DATABASE SALARY_DB;
GO

-- CREATE THE {SALARY_DB} DATABASE
CREATE DATABASE SALARY_DB;
GO

-- MAKE THE {SALARY_DB} THE ACTIVE DATABASE
USE SALARY_DB;
GO

-- CREATE {tblEmployee} TABLE
EXECUTE [dbo].[SP_Create_Employee_Table];
GO

-- FUNCTION TO CALCULATE NET INCOME FROM GROSS INCOME (Formula: 2023)
-- @PARAM	{MONEY} @gross_salary	- TOTAL OF ALL INCOMES
-- @PARAM	{FLOAT} @itex			- INCOME-TAX PERCENTAGE
-- @PARAM	{FLOAT} @cpp			- CPP PERCENTAGE
-- @PARAM	{FLOAT} @ei				- EI PERCENTAGE
-- @RETURN	{TABLE}	@result			- TABLE OF EI, CPP, TAX, and NET-SALARY
CREATE OR ALTER FUNCTION dbo.FN_Calculate (
	@gross_salary		MONEY,			-- REQUIRED
	@itax				FLOAT = NULL,	-- OPTIONAL, CUSTOM INCOME-TAX PERCENTAGE
	@cpp				FLOAT = NULL,	-- OPTIONAL, CUSTOM CPP PERCENTAGE
	@ei					FLOAT = NULL	-- OPTIONAL, CUSTOM EI PERCENTAGE
) RETURNS @result TABLE (
	total_tax			MONEY,
	cpp_deductions		MONEY,
	ei_deductions		MONEY,
	after_tax_salary	MONEY
) AS BEGIN
	-- IF CALLER PROVIDES CUSTOM PERCENTAGES FOR EI, CPP, AND ITAX
	IF @itax IS NOT NULL AND @cpp IS NOT NULL AND @ei IS NOT NULL BEGIN
		DECLARE @t_tax	MONEY = @gross_salary * @itax;
		DECLARE @t_cpp	MONEY = @gross_salary * @cpp;
		DECLARE @t_ei	MONEY = @gross_salary * @ei;
		-- POPULATE RESULT
		INSERT @result
		SELECT @t_tax, @t_cpp, @t_ei, @gross_salary - @t_tax - @t_cpp - @t_ei;
		-- RETURN EARLY WITH RESULT SET
		RETURN;
	END

	-- OTHERWISE, CALCULATE USING STANDERD METHOD
	-- EI PREMIUMS
	DECLARE @ei_premiums			MONEY = dbo.Find_Max(0, dbo.Find_Min(@gross_salary, 61500)) * 0.0158;
	-- CPP PREMIUMS
	DECLARE @cpp_premiums			MONEY = (dbo.Find_Max(0, dbo.Find_Min(@gross_salary, 66600) - 3500) * 0.163) / 2;
	-- CPP DEDUCTIONS
	DECLARE @cpp_deductions			MONEY = dbo.Find_Max(0, dbo.Find_Min(@gross_salary, 66600) - 3500) * 0.0075;
	-- PAYROLL DEDUCTIONS
	DECLARE @payroll_deductions		MONEY = @cpp_premiums + @ei_premiums;
	-- PAYROLL TAX DEDUCTIONS
	DECLARE @payroll_tax_credits	MONEY = @cpp_premiums - @cpp_deductions + @ei_premiums;
	-- INCOME
	DECLARE @income					MONEY = @gross_salary + @cpp_deductions;

	-- FEDERAL TAX
	DECLARE @f_tax MONEY;
	IF @income <= 50197
		SET @f_tax = @income * 0.15;
	ELSE IF @income <= 100392
		SET @f_tax = (@income - 50197) * 0.205 + 7529.55;
	ELSE IF @income <= 155625
		SET @f_tax = (@income - 100392) * 0.26 + 17819.53;
	ELSE IF @income <= 221708
		SET @f_tax = (@income - 155625) * 0.29 + 32180.11;
	ELSE
		SET @f_tax = (@income - 221708) * 0.33 + 51344.18;

	-- FEDERAL BPA
	DECLARE @fed_bpa MONEY = 12719;
	IF @income < 155625
		SET @fed_bpa += 1679;
	ELSE IF @income < 221708
		SET @fed_bpa += 1679 - (@income - 155625) * 0.025407442;

	-- APPLY FEDERAL BPA AND TAX
	SET @f_tax = dbo.Find_Max(@f_tax - (@fed_bpa + dbo.Find_Min(1287, @gross_salary) + @payroll_tax_credits) * 0.15, 0);

	-- PROVINCIAL TAX
	DECLARE @p_tax MONEY;
	IF @income <= 46226
		SET @p_tax = @income * 0.0505;
	ELSE IF @income <= 92454
		SET @p_tax = (@income - 46226) * 0.0915 + 2334.41;
	ELSE IF @income <= 150000
		SET @p_tax = (@income - 92454) * 0.1116 + 6564.28;
	ELSE IF @income <= 220000
		SET @p_tax = (@income - 150000) * 0.1216 + 12986.41;
	ELSE
		SET @p_tax = (@income - 220000) * 0.1316 + 21498.41;

	-- APPLY PROVINCIAL TAX
	SET @p_tax = dbo.Find_Max(@p_tax - (11141 + @payroll_tax_credits) * 0.0505, 0);

	-- ONTARIO SURTAX
	DECLARE @s_tax MONEY;
	IF @p_tax >= 6387
		SET @s_tax = (@p_tax - 4991) * 0.2 + (@p_tax - 6387) * 0.36;
	ELSE IF @p_tax >= 4991
		SET @s_tax = (@p_tax - 4991) * 0.2;
	ELSE
		SET @s_tax = 0;

	-- ONTARIO DTC AFTER SURTAX
	SET @p_tax += @s_tax;

	-- ONTARION HEALTH PREMIUM
	DECLARE @on_health MONEY = 0;
	IF @income > 200600
		SET @on_health = 900;
	ELSE IF @income > 200000
		SET @on_health = (@income - 200000) * 0.25 + 750;
	ELSE IF @income > 72600
		SET @on_health = 750;
	ELSE IF @income > 72000
		SET @on_health = (@income - 72000) * 0.25 + 600;
	ELSE IF @income > 48600
		SET @on_health = 600;
	ELSE IF @income > 48000
		SET @on_health = (@income - 48000) * 0.25 + 450;
	ELSE IF @income > 38500
		SET @on_health = 450;
	ELSE IF @income > 36000
		SET @on_health = (@income - 36000) * 0.06 + 300;
	ELSE IF @income > 25000
		SET @on_health = 300;
	ELSE IF @income > 20000
		SET @on_health = (@income - 20000) * 0.06 + 0;
	SET @p_tax += @on_health;

	-- TOTAL TAX AMOUNT
	DECLARE @total_tax MONEY = @f_tax + @p_tax + @payroll_deductions;

	-- NET INCOME
	DECLARE @after_tax_income MONEY = @gross_salary - @total_tax;

	-- POPULATE RETURNABLE TABLE
	INSERT @result
	SELECT @total_tax, @payroll_deductions, @ei_premiums, @after_tax_income;

	RETURN;
END
GO

-- FUNCTION TO FIND MAX OF TWO NUMBERS
-- @PARAM	{MONEY} @value1 FIRST NUMBER
-- @PARAM	{MONEY} @value2 SECOND NUMBER
-- @RETURN	{MONEY} MAXIMUM OF TWO GIVEN NUMBERS
CREATE OR ALTER FUNCTION dbo.Find_Max (
	@value1 MONEY,
	@value2 MONEY
) RETURNS MONEY
AS BEGIN
	IF @value1 >= @value2
		RETURN @value1;
	RETURN @value2;
END
GO

-- FUNCTION TO FIND MIN OF TWO NUMBERS
-- @PARAM	{MONEY} @value1 FIRST NUMBER
-- @PARAM	{MONEY} @value2 SECOND NUMBER
-- @RETURN	{MONEY} MINIMUM OF TWO GIVEN NUMBERS
CREATE OR ALTER FUNCTION dbo.Find_Min (
	@value1 MONEY,
	@value2 MONEY
) RETURNS MONEY
AS BEGIN
	IF @value1 <= @value2
		RETURN @value1;
	RETURN @value2;
END
GO

-- CREATE ONCE {#TempTable}
IF NOT EXISTS(
	SELECT [name]
	FROM [tempdb].[sys].[tables]
	WHERE [name] LIKE '#TempTable%'
) BEGIN
	-- CREATE TEMPORARY TABLE TO TRACK INDIVIDUAL RECORDS OF EMPLOYEE TABLE
	-- THE GENERATED IDENTITY {key} IS USED TO FETCH ACTUAL ROW FROM EMPLOYEE TABLE
	CREATE TABLE #TempTable (
		e_id INT IDENTITY(1, 1),
		emp_id INT
	);

	-- POPULATE TABLE WITH {emp_id} OF SOURCE TABLE AND GENERATED {key} FROM IDENTITY
	INSERT INTO #TempTable(emp_id)
	SELECT dbo.tblEmployee.emp_id
	FROM dbo.tblEmployee;
END
GO

-- CREATE ONCE {#ResultSet}
IF NOT EXISTS(
	SELECT [name]
	FROM [tempdb].[sys].[tables]
	WHERE [name] LIKE '#ResultSet%'
) BEGIN
	-- TEMPORARY TABLE TO HOLD DISPLAYABLE CALCULATIONS
	CREATE TABLE #ResultSet (
		[EMPLOYEE NAME]				CHAR(100),
		[GENDER]					CHAR(10),
		[DEPENDANTS]				TINYINT,
		[GROSS SALARY + ADDITIONS]	MONEY,
		[TOTAL TAX]					MONEY,
		[CPP]						MONEY,
		[EI]						MONEY,
		[NET SALARY]				MONEY
	);
END
GO

-- A POINTER TO CUTTENT TABLE ROW
DECLARE @COUNT INT;
SELECT TOP(1) @COUNT = e_id
FROM #TempTable;

-- MAXIMUM NUMBER OF ROWS IN {#TempTable}, SO IN {dbo.tblEmployee}
DECLARE @MAX INT;
SELECT @MAX = COUNT(0)
FROM #TempTable;

-- HOLD DATA FROM {tblEmployee} TABLE FOR EACH ROW
DECLARE @gender_relief 	TINYINT;
DECLARE @deps_relief 	TINYINT;
DECLARE @gross_salary 	MONEY;

-- HOLD CALCULATION DATA FROM {FN_Calculate} FUNCTION
DECLARE	@tax 	MONEY;
DECLARE	@cpp 	MONEY;
DECLARE	@ei 	MONEY;
DECLARE	@salary MONEY;

-- ITERATE UNTILL ALL ROWS PROCESSED
WHILE @COUNT <= @MAX
BEGIN
	-- FETCH AND STORE ACTUAL {emp_id} USING {#TempTable} REFERENCE
	DECLARE @emp_id INT;
	SELECT  @emp_id = emp_id
	FROM #TempTable
	WHERE e_id = @COUNT;

	-- FETCH AND IDENTIFY PARAMETER VALUES TO BE USED FOR CALCULATION
	SELECT	@gross_salary	= GrossSalary + Additions,
			@gender_relief	= (CASE emp_gender WHEN 'Male' THEN 0 ELSE 1 END),
			@deps_relief	= (CASE WHEN noOfDependants <= 2 THEN 0 WHEN noOfDependants = 3 THEN 2 ELSE 4 END)
	FROM dbo.tblEmployee
	WHERE emp_id = @emp_id;

	-- HOLD CALCULATED RESULT OF {FN_Calculate} FUNCTION: TAX, CPP, EI, AND SALARY
	SELECT
		@tax 	= total_tax,
		@cpp 	= cpp_deductions,
		@ei 	= ei_deductions,
		@salary = after_tax_salary
	FROM dbo.FN_Calculate(@gross_salary, DEFAULT, DEFAULT, DEFAULT);

	-- APPLY GENDER BENEFITS (IF-ANY)
	SET @salary *= (1 + (@gender_relief / 100.0));

	-- APPLY DEPENDANTS BENEFITS (IF-ANY)
	SET @salary *= (1 + @deps_relief / 100.0);

	-- STORE ALL DATA INTO {#ResultSet} TABLE
	INSERT #ResultSet
	SELECT emp_name, emp_gender, noOfDependants, @gross_salary, @tax, @cpp, @ei, @salary
	FROM dbo.tblEmployee
	WHERE emp_id = @emp_id;

	-- POINT TO NEXT ROW
	SET @COUNT += 1;
END
GO

-- DISPLAY RESULT
SELECT * FROM #ResultSet;
GO

-- EMPTY {#ResultSet} TABLE DATA
TRUNCATE TABLE #ResultSet;
GO

-- DISPOSE EVERYTHING GENERATED BY THIS SCRIPT (Find_Max, Find_Min, FN_Calculate, #TempTable, #ResultSet)
-- CHANGE TO FALSELY CONDITION TO DISABLE CLEANUP...
IF 1 = 1
BEGIN
	DROP FUNCTION  IF EXISTS dbo.Find_Min;
	DROP FUNCTION  IF EXISTS dbo.Find_Max;
	DROP FUNCTION  IF EXISTS dbo.FN_Calculate;

	-- OPTIONAL
	DROP PROCEDURE IF EXISTS dbo.SP_Create_Employee_Table;

	IF EXISTS(
		SELECT [name]
		FROM [tempdb].[sys].[tables]
		WHERE [name] LIKE '#TempTable%'
	) DROP TABLE #TempTable;

	IF EXISTS(
		SELECT [name]
		FROM [tempdb].[sys].[tables]
		WHERE [name] LIKE '#ResultSet%'
	) DROP TABLE #ResultSet;
END

-- EXECUTE TO RE-CREATE {tblEmployee}
CREATE OR ALTER PROCEDURE dbo.SP_Create_Employee_Table AS
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE IF EXISTS [dbo].[tblEmployee];
GO

CREATE TABLE [dbo].[tblEmployee](
	[emp_id] 			[int] IDENTITY(1,1) NOT NULL,
	[emp_name] 			[varchar](60) 			NULL,
	[emp_email] 		[varchar](40) 			NULL,
	[emp_password] 		[varchar](30) 			NULL,
	[emp_gender] 		[varchar](15) 			NULL,
	[noOfDependants] 	[int] 					NULL,
	[Additions] 		[money] 				NULL,
	[ITex] 				[decimal](3, 2) 		NULL,
	[CPP] 				[decimal](3, 2) 		NULL,
	[EI] 				[decimal](3, 2) 		NULL,
	[GrossSalary] 		[money] 				NULL,
	PRIMARY KEY CLUSTERED ([emp_id] ASC)
	WITH (
		PAD_INDEX 					= OFF,
		STATISTICS_NORECOMPUTE 		= OFF,
		IGNORE_DUP_KEY 				= OFF,
		ALLOW_ROW_LOCKS 			=  ON,
		ALLOW_PAGE_LOCKS 			=  ON,
		OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
	) ON [PRIMARY]
) ON [PRIMARY]
GO

SET IDENTITY_INSERT [dbo].[tblEmployee] ON;
GO

INSERT [dbo].[tblEmployee]
([emp_id], 	[emp_name], 		[emp_email], 				[emp_password], [emp_gender], 	[noOfDependants], 	[Additions], 	[ITex], 						[CPP], 							[EI], 							[GrossSalary])
VALUES
(2, 		N'Karry', 			N'KKhall@yahoo.com', 		NULL, 			N'Male', 		4, 					325.0000, 		CAST(0.21 AS Decimal(3, 2)), 	CAST(0.07 AS Decimal(3, 2)), 	CAST(0.05 AS Decimal(3, 2)), 	81000.0000),
(1148, 		N'Sujawal', 		N'sujawalfff@yahoo.com', 	NULL, 			N'Female', 		1, 					650.0000, 		CAST(0.25 AS Decimal(3, 2)), 	CAST(0.04 AS Decimal(3, 2)), 	CAST(0.03 AS Decimal(3, 2)), 	77000.0000),
(2200, 		N'Allana', 			N'Allana@gmail.com', 		NULL, 			N'Female', 		3, 					280.0000, 		CAST(0.28 AS Decimal(3, 2)), 	CAST(0.05 AS Decimal(3, 2)), 	CAST(0.04 AS Decimal(3, 2)), 	12000.0000),
(2204, 		N'John', 			N'jadde@yahoo.com', 		NULL, 			N'Male', 		2, 					180.0000, 		CAST(0.28 AS Decimal(3, 2)), 	CAST(0.24 AS Decimal(3, 2)), 	CAST(0.04 AS Decimal(3, 2)), 	55000.0000),
(2205, 		N'Sally', 			N'salley@yahoo.com', 		NULL, 			N'Female', 		3, 					250.0000, 		CAST(0.28 AS Decimal(3, 2)), 	CAST(0.04 AS Decimal(3, 2)), 	CAST(0.05 AS Decimal(3, 2)), 	58000.0000),
(2207, 		N'Jane', 			N'jane@yahoo.com', 			NULL, 			N'Male', 		4, 					150.0000, 		CAST(0.05 AS Decimal(3, 2)), 	CAST(0.04 AS Decimal(3, 2)), 	CAST(0.04 AS Decimal(3, 2)), 	79000.0000),
(2209, 		N'Jiggar', 			N'jghdd25@yahoo.com', 		NULL, 			N'Male', 		3, 					160.0000, 		CAST(0.25 AS Decimal(3, 2)), 	CAST(0.05 AS Decimal(3, 2)), 	CAST(0.04 AS Decimal(3, 2)), 	78000.0000),
(2212, 		N'Nattaly Hill ', 	N'natilli@yahoo.com', 		NULL, 			N'Female', 		3, 					180.0000, 		CAST(0.28 AS Decimal(3, 2)), 	CAST(0.05 AS Decimal(3, 2)), 	CAST(0.03 AS Decimal(3, 2)), 	78500.0000);

SET IDENTITY_INSERT [dbo].[tblEmployee] OFF;
GO
