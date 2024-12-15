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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            var products = await _repository.GetProductsAsync();
            return products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                RegularPrice = p.RegularPrice,
                Discount = p.Discount,
                DiscountedPrice = p.DiscountedPrice,
                SubcategoryId = p.SubcategoryId,
                SubcategoryName = p.Subcategory?.Name ?? string.Empty,
                UnitType = p.UnitType,
                DiscountStartDate = p.DiscountStartDate,
                DiscountEndDate = p.DiscountEndDate,
                Image = p.Image
            });
        }

        public async Task<ProductDTO?> GetProductByIdAsync(int id)
        {
            var product = await _repository.GetProductByIdAsync(id);
            if (product == null) return null;

            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                RegularPrice = product.RegularPrice,
                Discount = product.Discount,
                DiscountedPrice = product.DiscountedPrice,
                SubcategoryId = product.SubcategoryId,
                SubcategoryName = product.Subcategory?.Name ?? string.Empty,
                UnitType = product.UnitType,
                DiscountStartDate = product.DiscountStartDate,
                DiscountEndDate = product.DiscountEndDate,
                Image = product.Image
            };
        }

        public async Task<int> AddProductAsync(ProductDTO productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                RegularPrice = productDto.RegularPrice,
                Discount = productDto.Discount,
                SubcategoryId = productDto.SubcategoryId,
                UnitType = productDto.UnitType,
                DiscountStartDate = productDto.DiscountStartDate,
                DiscountEndDate = productDto.DiscountEndDate,
                Image = productDto.Image
            };

            return await _repository.AddProductAsync(product);
        }

        public async Task UpdateProductAsync(ProductDTO productDto)
        {
            var product = new Product
            {
                Id = productDto.Id,
                Name = productDto.Name,
                RegularPrice = productDto.RegularPrice,
                Discount = productDto.Discount,
                SubcategoryId = productDto.SubcategoryId,
                UnitType = productDto.UnitType,
                DiscountStartDate = productDto.DiscountStartDate,
                DiscountEndDate = productDto.DiscountEndDate,
            };

            await _repository.UpdateProductAsync(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            await _repository.DeleteProductAsync(id);
        }
    }
}
