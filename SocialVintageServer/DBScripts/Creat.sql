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
UserMail nvarchar(50) unique not null,
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

create table ItemsImages 
(
Id int primary key identity,
ItemId int not null,
FOREIGN KEY (ItemId) REFERENCES Item(ItemId)
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
INSERT INTO Catagory (CatagoryName) Values ('unisex');
SELECT * FROM Catagory;

INSERT INTO [Status] (StatusName) Values ('Approved');
INSERT INTO [Status] (StatusName) Values ('Pending');
INSERT INTO [Status] (StatusName) Values ('Delivered');
SELECT * FROM [Status];

Insert Into [User] Values('admin', 'amit@gmail.com', '1011', 'yeshurun 40 hod hasharon', 1);
Insert Into [User] Values('ofer', 'ofer@gmail.com', '1011', 'yeshurun 40 hod hasharon', 0);
SELECT * FROM [User];


insert into Store (StoreId, StoreName, Adress, OptionId, CatagoryId) values (1, 'Amit Store', 'Yeshuron, 40, Hod Hasharon, Israel', 1, 1)
Go

insert into Item values ('medium', 'Nike', 'Red', 100, 1, 'T Shirt')
Go

insert into Item values ('medium', 'zara', 'pink', 45, 1, 'strapless pink top')
Go

insert into ItemsImages values (1)
go

insert into ItemsImages values (2)
go

SELECT * FROM [User];
select * from Item
select * from ItemsImages
CREATE LOGIN [AdminLogin] WITH PASSWORD = 'amitbe1011!';
Go

CREATE USER [AdminUser] FOR LOGIN [AdminLogin];
Go

ALTER ROLE db_owner ADD MEMBER [AdminUser];
Go

--scaffold-DbContext "Server = (localdb)\MSSQLLocalDB;Initial Catalog=SocialVintageDB;User ID=AdminLogin;Password=amitbe1011!;" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Models -Context SocialVintageDbContext -DataAnnotations –force
