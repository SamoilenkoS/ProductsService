CREATE TABLE [dbo].[Products]
(
    [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [Title] NVARCHAR(50) NOT NULL,
    [Price] NUMERIC(9, 2) NOT NULL,
    [CategoryId] INT NOT NULL,
    [IsAvailableToBuy] BIT NOT NULL,
    CONSTRAINT FK_Category FOREIGN KEY (CategoryId) REFERENCES Categories(Id)
)