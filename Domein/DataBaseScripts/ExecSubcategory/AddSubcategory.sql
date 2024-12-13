ALTER PROCEDURE [dbo].[AddSubcategory]
    @Name NVARCHAR(255),
    @CategoryId INT
AS
BEGIN
    INSERT INTO Subcategories (Name, CategoryId)
    VALUES (@Name, @CategoryId);

    SELECT SCOPE_IDENTITY() AS NewSubcategoryId; -- Возвращает ID добавленной подкатегории
END;