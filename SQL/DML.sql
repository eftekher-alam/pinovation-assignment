INSERT INTO TblCountry
VALUES
('Bangladesh'),
('India')
GO

INSERT INTO TblCity
VALUES
('Dhaka', 1),
('Rajshahi', 1),
('New Delhi', 2),
('Moddha Pradesh', 2)
GO

INSERT INTO TblUsers
VALUES
(1, 'Eftekher', 'Shuvo', 01790303459, 'eftekher@gmail.com', 1, '\Picture1.png', '\cv1.pdf', '123434', '10-08-1997'),
(1, 'Lipi', 'Akter', 01590303459, 'lipi@gmail.com', 2, '\Picture2.png', '\cv3.pdf', 'asdf', '02-08-1995')
GO


EXEC sp_GetUsers
GO

Exec sp_GetUserById 4
GO

EXEC sp_GetUserByEmail 'l@gmail.com'
GO



EXEC sp_UpdateUser 4, 'Efte', NULL , 01790303459, 'e@gmail.com', 1, NULL, NULL, '123434', '10-08-1997'
GO

EXEC sp_IsEmailUnique 'eX@gmail.com'
GO

EXEC sp_InsertUser 3100000, 'Eftekher', NULL , 01790303459, 'e@gmail.com', 1, NULL, NULL, '123434', '10-08-1997'
GO


EXEC sp_GetCountry
GO


EXEC sp_GetCityByCountryId 1
GO