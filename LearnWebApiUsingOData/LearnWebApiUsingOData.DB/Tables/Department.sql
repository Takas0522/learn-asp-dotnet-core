CREATE TABLE [dbo].[Department]
(
    [CompanyId] INT NOT NULL,
    [DepartmentId] INT NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_Department_ToCompany] FOREIGN KEY ([CompanyId]) REFERENCES [Company]([Id]), 
    CONSTRAINT [PK_Department] PRIMARY KEY ([CompanyId], [DepartmentId]) 
)

