ALTER PROCEDURE [dbo].[GetProductById]
    @Id INT
AS
BEGIN
    SELECT 
        p.Id, 
        p.Name, 
        p.RegularPrice, 
        p.Discount, 
        p.DiscountedPrice, 
        p.UnitType, 
        p.DiscountStartDate, 
        p.DiscountEndDate, 
        p.Image,              
        p.SubcategoryId, 
        sc.Name AS SubcategoryName, 
        sc.CategoryId, 
        c.Name AS CategoryName
    FROM Products p
    INNER JOIN Subcategories sc ON p.SubcategoryId = sc.Id
    INNER JOIN Categories c ON sc.CategoryId = c.Id
    WHERE p.Id = @Id;
END;