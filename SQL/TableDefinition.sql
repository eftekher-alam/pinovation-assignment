CREATE DATABASE PinovationAssignment
GO

USE PinovationAssignment
GO

CREATE TABLE TblCountry 
(
	countryId INT PRIMARY KEY IDENTITY(1, 1),
	countryName NVARCHAR(100) NOT NULL,
)
GO

CREATE TABLE TblCity
(
	cityId INT PRIMARY KEY IDENTITY(1, 1),
	cityName NVARCHAR(100) NOT NULL,
	countryId INT REFERENCES TblCountry(countryId)
)
GO

CREATE TABLE TblUsers
(
	userId INT PRIMARY KEY,
	fName NVARCHAR(100) NOT NULL,
	lName NVARCHAR(100),
	phoneNo BIGINT NOT NULL,
	emailNo NVARCHAR(250) UNIQUE NOT NULL,
	userCity INT REFERENCES TblCity(cityId),
	userImg NVARCHAR(3000),
	userCV NVARCHAR(3000),
	password NVARCHAR(500),
	dob DATETIME
)
GO



