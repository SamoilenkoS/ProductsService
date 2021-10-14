/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

insert into Categories (Title) values ('Food');
insert into Categories (Title) values ('Other');

declare @id int = (select Id from Categories where Title = 'Food');

insert into Products values (NEWID(), 'Milk', 28, @id, 1);