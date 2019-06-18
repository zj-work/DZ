--DataBase Script

--Create database
USE master
GO

IF NOT EXISTS(SELECT * FROM dbo.sysdatabases WHERE name='DB_Main')
BEGIN
	CREATE DATABASE DB_Main
END
GO

USE DB_Main
GO

--Create Tables
IF NOT EXISTS(SELECT * FROM dbo.sysobjects WHERE id=OBJECT_ID('tbl_log'))
BEGIN
	CREATE TABLE dbo.tbl_log
	(
		id INT IDENTITY PRIMARY KEY NOT NULL,
		type NVARCHAR(50) NOT NULL,
		content NVARCHAR(MAX) NULL,
		createtime DATETIME NOT NULL DEFAULT GETDATE(),
		ip NVARCHAR(200) NULL,
		remarks NVARCHAR(MAX) NULL
	)
END
ELSE
BEGIN
	PRINT 'table tbl_log exist'
END
