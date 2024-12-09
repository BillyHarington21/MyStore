using Domein.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domein.Repositories
{
    public interface ISubcategoryRepository
    {
        Task<IEnumerable<Subcategory>> GetSubcategoriesAsync();
        Task<Subcategory?> GetSubcategoryByIdAsync(int id);
        Task<int> AddSubcategoryAsync(string name, int categoryId);
        Task UpdateSubcategoryAsync(int id, string name, int categoryId);
        Task DeleteSubcategoryAsync(int id);
    }
}
