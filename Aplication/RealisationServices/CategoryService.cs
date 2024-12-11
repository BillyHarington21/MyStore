using Aplication.DTO;
using Aplication.Services;
using Domein.Entitys;
using Domein.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.RealisationServices
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = await _repository.GetCategoriesAsync();
            return categories.Select(c => new CategoryDTO
            {
                Id = c.Id,
                Name = c.Name
            });
        }

        public async Task<CategoryDTO?> GetCategoryByIdAsync(int id)
        {
            var category = await _repository.GetCategoryByIdAsync(id);
            return category == null ? null : new CategoryDTO    
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public async Task<int> AddCategoryAsync(CategoryDTO categoryDto)
        {
            var category = new Category
            {
                Name = categoryDto.Name
            };
            return await _repository.AddCategoryAsync(category.Name);
        }

        public async Task UpdateCategoryAsync(CategoryDTO categoryDto)
        {
            var category = new Category
            {
                Id = categoryDto.Id,
                Name = categoryDto.Name
            };

            await _repository.UpdateCategoryAsync(category.Id, category.Name);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await _repository.DeleteCategoryAsync(id);
        }
    }
}
