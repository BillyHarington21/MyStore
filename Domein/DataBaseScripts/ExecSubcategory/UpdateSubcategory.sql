ALTER PROCEDURE [dbo].[UpdateSubcategory]
    @Id INT,
    @Name NVARCHAR(255),
    @CategoryId INT
AS
BEGIN
    UPDATE Subcategories
    SET Name = @Name,
        CategoryId = @CategoryId
    WHERE Id = @Id;
END;