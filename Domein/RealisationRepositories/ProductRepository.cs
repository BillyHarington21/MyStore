using Domein.Context;
using Domein.Entitys;
using Domein.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domein.RealisationRepositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products
                .FromSqlRaw("EXEC GetProducts")
                .ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products
                  .FromSqlRaw("SELECT * FROM Products WHERE Id = {0}", id)
                  .FirstOrDefaultAsync();
        }

        public async Task<int> AddProductAsync(Product product)
        {
            var result = await _context.Database.ExecuteSqlRawAsync(
                "EXEC AddProduct @Name={0}, @Price={1}, @Discount={2}," +
                "@DiscountStartDate={3}, @DiscountEndDate={4}, @UnitType={5}, @SubcategoryId={6}",
                product.Name, product.RegularPrice, product.Discount, product.DiscountStartDate, product.DiscountEndDate,
                product.UnitType, product.SubcategoryId);
            return result;
        }

        public async Task UpdateProductAsync(Product product)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC UpdateProduct @Id={0}, @Name={1}, @Price={2}, @Discount={3}," +
                "@DiscountStartDate={4}, @DiscountEndDate={5}, @UnitType={6}",
                product.Id, product.Name, product.RegularPrice, product.Discount, product.DiscountStartDate, product.DiscountEndDate,
                  product.UnitType);
        }


        public async Task DeleteProductAsync(int id)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC DeleteProduct @Id={0}", id);
        }
    }
}
