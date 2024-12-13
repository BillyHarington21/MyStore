ALTER PROCEDURE [dbo].[GetSubcategories]
AS
BEGIN
    SELECT 
        sc.Id, 
        sc.Name, 
        sc.CategoryId, 
        c.Name AS CategoryName
    FROM Subcategories sc
    INNER JOIN Categories c ON sc.CategoryId = c.Id;
END;