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
[OptionName] nvarchar not null,
);

CREATE TABLE Catagory 
(
--מה קטגורית הבגדים - נשים גברים וכו
CatagoryId int primary key identity,
[CatagoryName] nvarchar not null,
);

CREATE TABLE [Store]
(
StoreId int primary key REFERENCES [User](UserId),
StoreName nvarchar(50) not null,
Adress nvarchar(50) not null,
OptionId nvarchar(50) not null,
LogoExt nvarchar(5) not null, 
CatagoryId nvarchar not null,
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
StoreId nvarchar(50) not null,
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
UserId nvarchar(50) not null,
OrderDate date not null,
StatusId nvarchar(50) not null,
FOREIGN KEY (UserId) REFERENCES [User](UserId),
FOREIGN KEY (StatusId) REFERENCES [Status](StatusId)
);

CREATE TABLE Review
(
ReviewId int primary key identity,
StoreId nvarchar(50) not null,
UserId nvarchar(50) not null,
ranking nvarchar(10) not null,
Info nvarchar(70) not null,
FOREIGN KEY (UserId) REFERENCES [User](UserId),
FOREIGN KEY (StoreId) REFERENCES [Store](StoreId)
);

CREATE TABLE FavoriteStores
(
StoreId nvarchar(50) not null,
UserId nvarchar(50) not null,
FOREIGN KEY (UserId) REFERENCES [User](UserId),
FOREIGN KEY (StoreId) REFERENCES [Store](StoreId),
PRIMARY KEY (UserId, StoreId)
);

--INSERT INTO [Shipping] (OptionName) Values ('Delivery');
--INSERT INTO [Shipping] (OptionName) Values ('PickUp');
