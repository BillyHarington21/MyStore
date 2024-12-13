ALTER PROCEDURE [dbo].[GetCategories]
AS
BEGIN
    SELECT Id, Name 
    FROM Categories;
END;