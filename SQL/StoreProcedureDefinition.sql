CREATE OR ALTER PROCEDURE sp_GetUsers
AS
BEGIN
	SELECT * FROM TblUsers
	ORDER BY fName ASC
END
GO


CREATE OR ALTER PROCEDURE sp_GetUserById
@id INT
AS
BEGIN
	SELECT * FROM TblUsers 
	WHERE userId = @id
END



CREATE OR ALTER PROCEDURE sp_GetUserByEmail
@email NVARCHAR(100)
AS
BEGIN
	SELECT * FROM TblUsers 
	WHERE emailNo = @email
END



CREATE OR ALTER PROCEDURE sp_InsertUser
	@userId INT,
	@fName NVARCHAR(100),
	@lName NVARCHAR(100) = NULL,
	@phoneNo BIGINT,
	@emailNo NVARCHAR(250),
	@userCity INT,
	@userImg NVARCHAR(3000) = NULL,
	@userCV NVARCHAR(3000) = NULL,
	@password NVARCHAR(500),
	@dob DATETIME
AS
BEGIN
	INSERT INTO TblUsers
	VALUES
	(@userId, @fName, @lName, @phoneNo, @emailNo, @userCity, @userImg, @userCV, @password, @dob)
END
GO


CREATE OR ALTER PROCEDURE sp_UpdateUser
	@userId INT,
	@fName NVARCHAR(100),
	@lName NVARCHAR(100),
	@phoneNo BIGINT,
	@emailNo NVARCHAR(250),
	@userCity INT,
	@userImg NVARCHAR(3000),
	@userCV NVARCHAR(3000),
	@password NVARCHAR(500),
	@dob DATETIME
AS
BEGIN
	UPDATE TblUsers
	SET fName =	@fName, lName = @lName, phoneNo = @phoneNo, emailNo = @emailNo, userCity = @userCity, userImg = @userImg, userCV = @userCV, password = @password, dob = @dob
	WHERE userId = @userId
END
GO


CREATE OR ALTER PROCEDURE sp_IsEmailUnique
@email NVARCHAR(100)
AS 
BEGIN
	SELECT * FROM TblUsers
	WHERE emailNo = @email
END
GO

CREATE OR ALTER PROCEDURE sp_GetMaxUserId
AS
BEGIN
	SELECT MAX(userId) FROM TblUsers
END
GO



CREATE OR ALTER PROCEDURE sp_GetCity
AS
BEGIN
	SELECT * FROM TblCity
END
GO



CREATE OR ALTER PROCEDURE sp_GetCountry
AS
BEGIN
	SELECT * FROM TblCountry
END
GO


CREATE OR ALTER PROCEDURE sp_GetCityByCountryId
@countryId INT
AS
BEGIN
	SELECT * FROM TblCity
	WHERE countryId = @countryId
END
GO

