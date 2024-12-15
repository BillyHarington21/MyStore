ALTER PROCEDURE [dbo].[UpdateProduct]
    @Id INT,
    @Name NVARCHAR(255),
    @Price DECIMAL(10, 2),
    @Discount DECIMAL(5, 2),
    @UnitType NVARCHAR(50),
    @DiscountStartDate DATE,
    @DiscountEndDate DATE
AS
BEGIN
    DECLARE @SpecialPrice DECIMAL(10, 2);
    SET @SpecialPrice = @Price - (@Price * @Discount / 100);

    UPDATE Products
    SET Name = @Name,
        RegularPrice = @Price,
        Discount = @Discount,
        DiscountedPrice = @SpecialPrice,
        UnitType = @UnitType,
        DiscountStartDate = @DiscountStartDate,
        DiscountEndDate = @DiscountEndDate
    WHERE Id = @Id;
END;