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
                .FromSqlRaw("EXEC GetProductById @Id={0}", id)
                .FirstOrDefaultAsync();
        }

        public async Task<int> AddProductAsync(Product product)
        {
            var result = await _context.Database.ExecuteSqlRawAsync(
                "EXEC AddProduct @Name={0}, @Price={1}, @Discount={2}," +
                "@SubcategoryId={3}, @DiscountStartDate={4}, @DiscountEndDate={5}, @image={6}",
                product.Name, product.RegularPrice, product.Discount, product.SubcategoryId, product.DiscountStartDate, product.DiscountEndDate,
                product.Image);
            return result;
        }

        public async Task UpdateProductAsync(Product product)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC UpdateProduct @Id={0}, @Name={1}, @Price={2}, @Discount={3}," +
                "@SubcategoryId={4}, @DiscountStartDate={5}, @DiscountEndDate={5}, @image={6}",
                product.Id, product.Name, product.RegularPrice, product.Discount, product.SubcategoryId, product.DiscountStartDate, product.DiscountEndDate,
                  product.Image);
        }


        public async Task DeleteProductAsync(int id)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC DeleteProduct @Id={0}", id);
        }
    }
}
