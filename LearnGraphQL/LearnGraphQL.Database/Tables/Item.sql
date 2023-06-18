CREATE TABLE [dbo].[Item]
(
    [Id] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [CategoryType] INT NOT NULL, 
    [Price] MONEY NOT NULL
)
