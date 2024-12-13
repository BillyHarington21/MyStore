ALTER PROCEDURE [dbo].[GetSubcategoryById]
    @Id INT
AS
BEGIN
    SELECT 
        sc.Id, 
        sc.Name, 
        sc.CategoryId, 
        c.Name AS CategoryName
    FROM Subcategories sc
    INNER JOIN Categories c ON sc.CategoryId = c.Id
    WHERE sc.Id = @Id;
END;