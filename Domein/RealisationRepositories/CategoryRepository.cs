using Domein.Context;
using Domein.Entitys;
using Domein.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domein.RealisationRepositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _context.Categories
                .FromSqlRaw("EXEC GetCategories")
                .ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories
                .FromSqlRaw("EXEC GetCategoryById @Id={0}", id)
                .FirstOrDefaultAsync();
        }

        public async Task<int> AddCategoryAsync(Category category)
        {
            var result = await _context.Database.ExecuteSqlRawAsync("EXEC AddCategory @Name", new SqlParameter("@Name", category.Name));

            return result;
        }

        public async Task UpdateCategoryAsync(int id, string name)
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC UpdateCategory @Id={0}, @Name={1}", id, name);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC DeleteCategory @Id={0}", id);
        }
    }
}
