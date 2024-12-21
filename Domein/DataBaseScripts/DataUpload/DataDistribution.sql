
INSERT INTO Categories (Name)
SELECT DISTINCT Category
FROM TempData;


INSERT INTO Subcategories (Name, CategoryId)
SELECT DISTINCT SubCategory, c.Id
FROM TempData t
JOIN Categories c ON t.Category = c.Name;

INSERT INTO Products (Name, RegularPrice, Discount, DiscountedPrice, SubcategoryId)
SELECT ProductName, Price, Discount, SpecialPrice, sc.Id
FROM TempData t
JOIN Subcategories sc ON t.SubCategory = sc.Name;

UPDATE Products
SET Image = (SELECT TOP 1 Image FROM ProductImages WHERE ImageId = Products.Id)
WHERE EXISTS (SELECT 1 FROM TempImages WHERE ImageId = Products.Id);