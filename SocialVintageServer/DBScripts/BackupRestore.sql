-- REPLACE YOUR DATABASE NAME, LOGIN AND PASSWORD IN THE SCRIPT BELOW

USE master;
GO

-- Declare the database name
DECLARE @DatabaseName NVARCHAR(255) = 'SocialVintageDB';

-- Generate and execute the kill commands for all active connections
DECLARE @KillCommand NVARCHAR(MAX);

SET @KillCommand = (
    SELECT STRING_AGG('KILL ' + CAST(session_id AS NVARCHAR), '; ')
    FROM sys.dm_exec_sessions
    WHERE database_id = DB_ID(@DatabaseName)
);

IF @KillCommand IS NOT NULL
BEGIN
    EXEC sp_executesql @KillCommand;
    PRINT 'All connections to the database have been terminated.';
END
ELSE
BEGIN
    PRINT 'No active connections to the database.';
END
Go

IF EXISTS (SELECT * FROM sys.databases WHERE name = N'SocialVintageDB')
BEGIN
    DROP DATABASE SocialVintageDB;
END
Go

drop login [AdminLogin]
-- Create a login for the admin user
CREATE LOGIN [AdminLogin] WITH PASSWORD = 'amitbe1011!';
Go

--so user can restore the DB!
ALTER SERVER ROLE sysadmin ADD MEMBER [AdminLogin];
Go

CREATE Database SocialVintageDB;
Go

use SocialVintageDB
Go

CREATE USER [AdminUser] FOR LOGIN [AdminLogin];
Go

ALTER ROLE db_owner ADD MEMBER [AdminUser];
Go

--Home

               USE master;
             
                ALTER DATABASE SocialVintageDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                RESTORE DATABASE SocialVintageDB FROM DISK = 'C:\Users\amitb\source\repos\SocialVintageServer\SocialVintageServer\DBScripts\backup.bak' 
                WITH REPLACE,
                MOVE 'SocialVintageDB' TO 'C:\AmitB\SocialVintageDB_data.mdf',   
                MOVE 'SocialVintageDB_log' TO 'C:\AmitB\SocialVintageDB_log.ldf';     ;

                ALTER DATABASE SocialVintageDB SET MULTI_USER;
--SCHOOL
               --USE master;
             
               -- ALTER DATABASE SocialVintageDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
               -- RESTORE DATABASE SocialVintageDB FROM DISK = 'C:\Users\User\Source\Repos\SocialVintageServer\SocialVintageServer\wwwroot\..\DBScripts\backup.bak' 
               -- WITH REPLACE,
               -- MOVE 'SocialVintageDB' TO 'D:\AmitB\SocialVintageDB_data.mdf',   
               -- MOVE 'SocialVintageDB_log' TO 'D:\AmitB\SocialVintageDB_log.ldf';     ;

               -- ALTER DATABASE SocialVintageDB SET MULTI_USER;





--                SELECT d.name DatabaseName, f.name LogicalName,
--f.physical_name AS PhysicalName,
--f.type_desc TypeofFile
--FROM sys.master_files f
--INNER JOIN sys.databases d ON d.database_id = f.database_id
--GO


--select * from item

--update item set isAvailable = 1

