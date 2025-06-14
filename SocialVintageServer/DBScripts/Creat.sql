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
HasStore bit not null,
PhoneNumber nvarchar(50) not null
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
IsAvailable bit not null default(1),
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

CREATE TABLE [WishListItem]
(
UserId int foreign key references [User](UserId) not null,
ItemId int foreign key references [Item](ItemId) not null,
Primary key (UserId, ItemId)
);

ALTER TABLE [WishListItem] Add FakeCol bit default(0) NOT NULL
go

--CREATE TABLE [Order]
--(
--OrderId int primary key references Item(ItemId) not null,
--UserId int not null,
--OrderDate date not null,
--FOREIGN KEY (UserId) REFERENCES [User](UserId),
--);

INSERT INTO [Shipping] (OptionName) Values ('Delivery');
INSERT INTO [Shipping] (OptionName) Values ('PickUp');
SELECT * FROM [Shipping];

INSERT INTO Catagory (CatagoryName) Values ('Women');
INSERT INTO Catagory (CatagoryName) Values ('Men');
INSERT INTO Catagory (CatagoryName) Values ('unisex');
SELECT * FROM Catagory;


Insert Into [User] Values('admin', 'amit@gmail.com', '1011', 'yeshurun 40 hod hasharon', 1, '0546287507');
Insert Into [User] Values('hadas', 'hadas@gmail.com', 'hadas123', ' hod hasharon', 1, '0546282501');
Insert Into [User] Values('ofer', 'ofer@gmail.com', '1011', 'yeshurun 40 hod hasharon', 0, '0526344450');
SELECT * FROM [User];


insert into Store (StoreId, StoreName, Adress, OptionId, CatagoryId) values (1, 'Amit Store', 'Yeshuron, 40, Hod Hasharon, Israel', 1, 3)
insert into Store (StoreId, StoreName, Adress, OptionId, CatagoryId) values (2, 'hadas Store', 'Yeshuron, 40, Hod Hasharon, Israel', 1, 1)
Go

insert into Item values ('medium', 'Nike', 'Red', 100, 1, 'T Shirt',1)
Go

insert into Item values ('medium', 'zara', 'pink', 45, 1, 'strapless pink top',1)
Go

insert into Item values ('large', 'zara', 'blue', 80, 1, 'blue denim skirt',1)
Go

insert into ItemsImages values (1)
go

insert into ItemsImages values (2)
go

SELECT * FROM [User];
select * from [Item]
select * from WishListItem
select * from ItemsImages
select * from Store
CREATE LOGIN [AdminLogin] WITH PASSWORD = 'amitbe1011!';
Go

DELETE FROM [User] WHERE UserId='10'
UPDATE Item Set IsAvailable='1' Where ItemId = '4' 

CREATE USER [AdminUser] FOR LOGIN [AdminLogin];
Go

ALTER ROLE db_owner ADD MEMBER [AdminUser];
Go

--scaffold-DbContext "Server = (localdb)\MSSQLLocalDB;Initial Catalog=SocialVintageDB;User ID=AdminLogin;Password=amitbe1011!;" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Models -Context SocialVintageDbContext -DataAnnotations –force
