CREATE TABLE [dbo].[Employee]
(
    [CompanyId] INT NOT NULL,
    [DepartmentId] INT NOT NULL,
    [Id] INT NOT NULL PRIMARY KEY,
    [Name] NVARCHAR(50), 
    CONSTRAINT [FK_Employee_Company] FOREIGN KEY ([CompanyId]) REFERENCES [Company]([Id]),
    CONSTRAINT [FK_Employee_Depertment] FOREIGN KEY ([CompanyId],[DepartmentId]) REFERENCES [Department]([CompanyId],[DepartmentId])
)
