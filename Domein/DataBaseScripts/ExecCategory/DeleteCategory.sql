ALTER PROCEDURE [dbo].[DeleteCategory]
    @Id INT
AS
BEGIN
    DELETE FROM Categories
    WHERE Id = @Id;
END;