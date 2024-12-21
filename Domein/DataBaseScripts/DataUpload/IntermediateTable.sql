-- intermediate table for uploading data into it
CREATE TABLE TempData (
    Category NVARCHAR(255),
    SubCategory NVARCHAR(255),
    ProductName NVARCHAR(255),
    Price DECIMAL(10, 2),
    Discount DECIMAL(5, 2),
    SpecialPrice DECIMAL(10, 2)
);

-- intermediate table for uploading image into it
CREATE TABLE ProductImages (
    Id INT PRIMARY KEY IDENTITY(1,1), 
    Image VARBINARY(MAX) NOT NULL     
);

-- upload the data into an intermediate table
BULK INSERT TempData
FROM 'C:\Users\Константин\Desktop\Копия Products.csv'
WITH (
    FIELDTERMINATOR = ',', 
    ROWTERMINATOR = '\n',  
    FIRSTROW = 2           
);



