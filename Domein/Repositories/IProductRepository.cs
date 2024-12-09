using Domein.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domein.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task<int> AddProductAsync(string name, decimal price, decimal discount, int subcategoryId, string unitType, DateTime? discountStartDate, DateTime? discountEndDate);
        Task UpdateProductAsync(int id, string name, decimal price, decimal discount, int subcategoryId, string unitType, DateTime? discountStartDate, DateTime? discountEndDate);
        Task DeleteProductAsync(int id);
    }
}
