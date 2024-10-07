Use master
Go
IF EXISTS (SELECT * FROM sys.databases WHERE name = N'SocialVintageDB')
BEGIN
    DROP DATABASE SocialVintageDB;
END
Go
Create Database SocialVintageDB
Go
Use SocialVintageDB
Go
Create Table AppUsers
(  
	Id int Primary Key Identity,
	UserName nvarchar(50) Not Null,
	UserLastName nvarchar(50) Not Null,
	UserEmail nvarchar(50) Unique Not Null,
	UserPassword nvarchar(50) Not Null,
	IsManager bit Not Null Default 0
)
Insert Into AppUsers Values('admin', 'admin', 'kuku@kuku.com', '1234', 1)
Go
-- Create a login for the admin user
CREATE LOGIN [SocialVintageAdminLogin] WITH PASSWORD = 'thePassword';
Go

-- Create a user in the YourProjectNameDB database for the login
CREATE USER [SocialVintageAdminUser] FOR LOGIN [SocialVintageAdminLogin];
Go

-- Add the user to the db_owner role to grant admin privileges
ALTER ROLE db_owner ADD MEMBER [SocialVintageAdminUser];
Go


--scaffold-DbContext "Server = (localdb)\MSSQLLocalDB;Initial Catalog=SocialVintageDB;User ID=SocialVintageAdminLogin;Password=thePassword;" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Models -Context SocialVintageDbContext -DataAnnotations –force
