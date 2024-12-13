ALTER PROCEDURE [dbo].[AddProduct]
    @Name NVARCHAR(255),
    @Price DECIMAL(10, 2),
    @Discount DECIMAL(5, 2),
    @SubcategoryId INT,
    @UnitType NVARCHAR(50),
    @DiscountStartDate DATE,
    @DiscountEndDate DATE,
    @Image VARBINARY(MAX) = NULL
AS
BEGIN
    DECLARE @SpecialPrice DECIMAL(10, 2);
    SET @SpecialPrice = @Price - (@Price * @Discount / 100);

    INSERT INTO Products (Name, RegularPrice, Discount, DiscountedPrice, SubcategoryId, UnitType, DiscountStartDate, DiscountEndDate, Image)
    VALUES (@Name, @Price, @Discount, @SpecialPrice, @SubcategoryId, @UnitType, @DiscountStartDate, @DiscountEndDate, @Image);

    SELECT SCOPE_IDENTITY() AS NewProductId;
END;