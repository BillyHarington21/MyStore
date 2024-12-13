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
    public class SubcategoryRepository : ISubcategoryRepository
    {
        private readonly AppDbContext _context;

        public SubcategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Subcategory>> GetSubcategoriesAsync()
        {
            return await _context.Subcategories
                .FromSqlRaw("EXEC GetSubcategories")
                .ToListAsync();
        }

        public async Task<Subcategory?> GetSubcategoryByIdAsync(int id)
        {
            return await _context.Subcategories
                .FromSqlRaw("EXEC GetSubcategoryById @Id={0}", id)
                .FirstOrDefaultAsync();
        }

        public async Task<int> AddSubcategoryAsync(string name, int categoryId)
        {
            var result = await _context.Database.ExecuteSqlRawAsync(
                "EXEC AddSubcategory @Name={0}, @CategoryId={1}", name, categoryId);
            return result;
        }

        public async Task UpdateSubcategoryAsync(int id, string name, int categoryId)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC UpdateSubcategory @Id={0}, @Name={1}, @CategoryId={2}", id, name, categoryId);
        }

        public async Task DeleteSubcategoryAsync(int id)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC DeleteSubcategory @Id={0}", id);
        }
    }
}
