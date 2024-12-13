ALTER PROCEDURE [dbo].[UpdateCategory]
    @Id INT,
    @Name NVARCHAR(255)
AS
BEGIN
    UPDATE Categories
    SET Name = @Name
    WHERE Id = @Id;
END;