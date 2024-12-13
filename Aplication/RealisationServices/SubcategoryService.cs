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
    public class SubcategoryService : ISubcategoryService
    {
        private readonly ISubcategoryRepository _repository;

        public SubcategoryService(ISubcategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SubcategoryDTO>> GetAllSubcategoriesAsync()
        {
            var subcategories = await _repository.GetSubcategoriesAsync();
            return subcategories.Select(sc => new SubcategoryDTO
            {
                Id = sc.Id,
                Name = sc.Name,
                CategoryId = sc.CategoryId
            });
        }

        public async Task<SubcategoryDTO?> GetSubcategoryByIdAsync(int id)
        {
            var subcategory = await _repository.GetSubcategoryByIdAsync(id);
            return subcategory == null ? null : new SubcategoryDTO
            {
                Id = subcategory.Id,
                Name = subcategory.Name,
                CategoryId = subcategory.CategoryId
            };
        }

        public async Task<int> AddSubcategoryAsync(SubcategoryDTO subcategoryDto)
        {
            var subcategory = new Subcategory
            {
                Name = subcategoryDto.Name,
                CategoryId = subcategoryDto.CategoryId
            };
            return await _repository.AddSubcategoryAsync(subcategory.Name, subcategory.CategoryId);
        }

        public async Task UpdateSubcategoryAsync(SubcategoryDTO subcategoryDto)
        {
            var subcategory = new Subcategory
            {
                Id = subcategoryDto.Id,
                Name = subcategoryDto.Name,
                CategoryId = subcategoryDto.CategoryId
            };

            await _repository.UpdateSubcategoryAsync(subcategory.Id, subcategory.Name, subcategory.CategoryId);
        }

        public async Task DeleteSubcategoryAsync(int id)
        {
            await _repository.DeleteSubcategoryAsync(id);
        }
    }
}
