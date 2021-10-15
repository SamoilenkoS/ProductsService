CREATE TABLE [dbo].[Products]
(
    [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [Title] NVARCHAR(50) NOT NULL,
    [Price] FLOAT NOT NULL,
    [CategoryId] INT NOT NULL,
    [IsAvailableToBuy] BIT NOT NULL,
    CONSTRAINT FK_Category FOREIGN KEY (CategoryId) REFERENCES Categories(Id)
)