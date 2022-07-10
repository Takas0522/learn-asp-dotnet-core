CREATE PROCEDURE [dbo].[GetUser]
    @id int
AS
    SELECT
        U.Id,
        U.Name
    FROM
        [dbo].[User] U
    WHERE
        U.Id = @id