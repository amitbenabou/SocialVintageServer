Use master
Go

IF EXISTS (SELECT * FROM sys.databases WHERE name =
N'SocialVintageDB')

BEGIN

DROP DATABASE SocialVintageDB;

END

Go

Create Database SocialVintageDB
Go

Use SocialVintageDB 
Go

CREATE TABLE [User]
(
UserId int primary key identity,
UserName nvarchar(50) not null,
UserMail nvarchar(50) not null,
Pswrd nvarchar(50) not null,
UserAdress nvarchar(50) not null,
HasStore bit not null
);

CREATE TABLE [Shipping]
(
OptionId int primary key identity,
[OptionName] nvarchar(50) not null,
);

CREATE TABLE Catagory 
(
--מה קטגורית הבגדים - נשים גברים וכו
CatagoryId int primary key identity,
[CatagoryName] nvarchar(50) not null,
);

CREATE TABLE [Store]
(
StoreId int primary key REFERENCES [User](UserId),
StoreName nvarchar(50) not null,
Adress nvarchar(50) not null,
OptionId int not null,
LogoExt nvarchar(5) not null, 
CatagoryId int not null,
FOREIGN KEY (OptionId) REFERENCES [Shipping](OptionId),
FOREIGN KEY (CatagoryId) REFERENCES [Catagory](CatagoryId)
);

CREATE TABLE Item 
(
ItemId int primary key identity,
Size nvarchar(50) not null,
Brand nvarchar(50) not null,
Color nvarchar(50) not null,
Price nvarchar(50) not null,
StoreId int not null,
ItemInfo nvarchar(100) not null,
FOREIGN KEY (StoreId) REFERENCES [Store](StoreId)
);

CREATE TABLE [Status]
(
StatusId int primary key identity,
StatusName nvarchar(50) not null
);

CREATE TABLE [Order]
(
OrderId int primary key identity,
UserId int not null,
OrderDate date not null,
StatusId int not null,
FOREIGN KEY (UserId) REFERENCES [User](UserId),
FOREIGN KEY (StatusId) REFERENCES [Status](StatusId)
);

CREATE TABLE Review
(
ReviewId int primary key identity,
StoreId int not null,
UserId int not null,
ranking nvarchar(10) not null,
Info nvarchar(70) not null,
FOREIGN KEY (UserId) REFERENCES [User](UserId),
FOREIGN KEY (StoreId) REFERENCES [Store](StoreId)
);

CREATE TABLE FavoriteStores
(
StoreId int not null,
UserId int not null,
FOREIGN KEY (UserId) REFERENCES [User](UserId),
FOREIGN KEY (StoreId) REFERENCES [Store](StoreId),
PRIMARY KEY (UserId, StoreId)
);

INSERT INTO [Shipping] (OptionName) Values ('Delivery');
INSERT INTO [Shipping] (OptionName) Values ('PickUp');
SELECT * FROM [Shipping];

INSERT INTO Catagory (CatagoryName) Values ('Women');
INSERT INTO Catagory (CatagoryName) Values ('Men');
SELECT * FROM Catagory;

INSERT INTO [Status] (StatusName) Values ('Approved');
INSERT INTO [Status] (StatusName) Values ('Pending');
INSERT INTO [Status] (StatusName) Values ('Delivered');
SELECT * FROM [Status];

Insert Into [User] Values('admin', 'amit@gmail.com', '1011', 'yeshurun 40 hod hasharon', 1);
SELECT * FROM [User];

CREATE LOGIN [AdminLogin] WITH PASSWORD = 'amitbe1011!';
Go

CREATE USER [AdminUser] FOR LOGIN [AdminLogin];
Go

ALTER ROLE db_owner ADD MEMBER [AdminUser];
Go

