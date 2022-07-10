CREATE PROCEDURE [dbo].[RegisterUser]
    @id int,
    @name NVARCHAR(50)
AS
    MERGE INTO [dbo].[User]
    USING (
        SELECT
            @id as Id,
            @name as Name
    ) AS T ON ([dbo].[User].Id = T.Id)
    WHEN MATCHED THEN
        UPDATE SET
            [Name] = T.Name
    WHEN NOT MATCHED THEN
        INSERT (
            [Id],
            [Name]
        ) VALUES (
            T.Id,
            T.Name
        );