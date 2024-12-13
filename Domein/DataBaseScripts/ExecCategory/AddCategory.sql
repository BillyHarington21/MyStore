ALTER PROCEDURE [dbo].[AddCategory]
    @Name NVARCHAR(255)
AS
BEGIN
    INSERT INTO Categories (Name)
    VALUES (@Name);

    SELECT SCOPE_IDENTITY() AS NewCategoryId; -- Возвращает ID добавленной категории
END;