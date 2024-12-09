﻿using Domein.Context;
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

        public async Task<int> AddProductAsync(string name, decimal price, decimal discount, int subcategoryId, string unitType, DateTime? discountStartDate, DateTime? discountEndDate)
        {
            var result = await _context.Database.ExecuteSqlRawAsync(
                "EXEC AddProduct @Name={0}, @Price={1}, @Discount={2}, @SubcategoryId={3}, @UnitType={4}, @DiscountStartDate={5}, @DiscountEndDate={6}",
                name, price, discount, subcategoryId, unitType, discountStartDate, discountEndDate);
            return result;
        }

        public async Task UpdateProductAsync(int id, string name, decimal price, decimal discount, int subcategoryId, string unitType, DateTime? discountStartDate, DateTime? discountEndDate)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC UpdateProduct @Id={0}, @Name={1}, @Price={2}, @Discount={3}, @SubcategoryId={4}, @UnitType={5}, @DiscountStartDate={6}, @DiscountEndDate={7}",
                id, name, price, discount, subcategoryId, unitType, discountStartDate, discountEndDate);
        }


        public async Task DeleteProductAsync(int id)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC DeleteProduct @Id={0}", id);
        }
    }
}
