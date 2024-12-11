using Aplication.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services
{
    public interface ISubcategoryService
    {
        Task<IEnumerable<SubcategoryDTO>> GetAllSubcategoriesAsync();
        Task<SubcategoryDTO?> GetSubcategoryByIdAsync(int id);
        Task<int> AddSubcategoryAsync(SubcategoryDTO subcategoryDto);
        Task UpdateSubcategoryAsync(SubcategoryDTO subcategoryDto);
        Task DeleteSubcategoryAsync(int id);
    }
}
