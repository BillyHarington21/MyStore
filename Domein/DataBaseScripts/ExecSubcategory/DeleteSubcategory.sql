ALTER PROCEDURE [dbo].[DeleteSubcategory]
    @Id INT
AS
BEGIN
    DELETE FROM Subcategories
    WHERE Id = @Id;
END;