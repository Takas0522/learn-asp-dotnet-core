CREATE PROCEDURE [dbo].[DeleteUser]
    @id int
AS
    DELETE
    FROM
        [dbo].[User]
    WHERE
        Id = @id