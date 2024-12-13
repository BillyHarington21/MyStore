ALTER PROCEDURE [dbo].[UpdateProduct]
    @Id INT,
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

    UPDATE Products
    SET Name = @Name,
        RegularPrice = @Price,
        Discount = @Discount,
        DiscountedPrice = @SpecialPrice,
        SubcategoryId = @SubcategoryId,
        UnitType = @UnitType,
        DiscountStartDate = @DiscountStartDate,
        DiscountEndDate = @DiscountEndDate,
        Image = @Image
    WHERE Id = @Id;
END;