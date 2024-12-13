using Aplication.DTO;
using Aplication.Services;
using Domein.Entitys;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MyStore.Controllers
{
    [Controller]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ISubcategoryService _subcategoryService;

        public ProductController(IProductService productService, ICategoryService categoryService, ISubcategoryService subcategoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _subcategoryService = subcategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> MainPage1(int? categoryId, int? subcategoryId, string priceSort)
        {
            // Загружаем все данные
            var products = await _productService.GetAllProductsAsync();
            var subcategories = await _subcategoryService.GetAllSubcategoriesAsync();
            var categories = await _categoryService.GetAllCategoriesAsync();

            // Фильтрация по категории
            if (categoryId.HasValue && categoryId.Value > 0)
            {
                var subcategoryIds = subcategories
                    .Where(sc => sc.CategoryId == categoryId.Value)
                    .Select(sc => sc.Id)
                    .ToList();

                if (subcategoryId.HasValue && subcategoryId.Value > 0)
                {
                    // Если выбрана подкатегория, фильтруем по ней
                    products = products
                        .Where(p => p.SubcategoryId == subcategoryId.Value)
                        .ToList();
                }
                else
                {
                    // Если подкатегория не выбрана, фильтруем по всем подкатегориям категории
                    products = products
                        .Where(p => subcategoryIds.Contains(p.SubcategoryId))
                        .ToList();
                }
            }
            else if (subcategoryId.HasValue && subcategoryId.Value > 0)
            {
                // Если выбрана только подкатегория, фильтруем по её Id
                products = products
                    .Where(p => p.SubcategoryId == subcategoryId.Value)
                    .ToList();
            }

            if (!string.IsNullOrEmpty(priceSort))
            {
                switch (priceSort)
                {
                    case "asc":
                        products = products.OrderBy(p => p.RegularPrice).ToList();
                        break;

                    case "desc":
                        products = products.OrderByDescending(p => p.RegularPrice).ToList();
                        break;

                    case "under5":
                        products = products.Where(p => p.RegularPrice <= 5).ToList();
                        break;

                    case "under10":
                        products = products.Where(p => p.RegularPrice <= 10).ToList();
                        break;

                    case "under15":
                        products = products.Where(p => p.RegularPrice <= 15).ToList();
                        break;
                }
            }

                // Передаём данные в представление
                ViewBag.Category = categories;
            ViewBag.Subcategory = subcategories.Where(sc => !categoryId.HasValue || sc.CategoryId == categoryId).ToList();
            ViewBag.SelectedCategory = categoryId;
            ViewBag.SelectedSubcategory = subcategoryId;
            ViewBag.PriceSort = priceSort;

            return View("MainPage1", products);
        }

        [HttpGet]
        public async Task<IActionResult> DetailsProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> EditDeleteCategory()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return View(categories); // Передаем список категорий в представление
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(string Name)
        {
            var newCategory = new CategoryDTO
            {
                Name = Name
            };
                await _categoryService.AddCategoryAsync(newCategory);
                return RedirectToAction("MaimPage1"); // Возвращаемся к списку категорий                        
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return RedirectToAction("MaimPage1"); // Возвращаемся к списку категорий
        }


    }
}
