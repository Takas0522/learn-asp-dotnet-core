CREATE PROCEDURE [dbo].[GetUsers] AS
    SELECT
        U.Id,
        U.Name
    FROM
        [dbo].[User] U