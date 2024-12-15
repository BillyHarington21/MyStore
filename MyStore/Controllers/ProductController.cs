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
            
            var products = await _productService.GetAllProductsAsync();
            var subcategories = await _subcategoryService.GetAllSubcategoriesAsync();
            var categories = await _categoryService.GetAllCategoriesAsync();

            
            if (categoryId.HasValue && categoryId.Value > 0)
            {
                var subcategoryIds = subcategories
                    .Where(sc => sc.CategoryId == categoryId.Value)
                    .Select(sc => sc.Id)
                    .ToList();

                if (subcategoryId.HasValue && subcategoryId.Value > 0)
                {
                    products = products
                        .Where(p => p.SubcategoryId == subcategoryId.Value)
                        .ToList();
                }
                else
                {
                    products = products
                        .Where(p => subcategoryIds.Contains(p.SubcategoryId))
                        .ToList();
                }
            }
            else if (subcategoryId.HasValue && subcategoryId.Value > 0)
            {
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

            return View(categories);
        }

        [HttpPost]
        public async Task<IActionResult> EditDeleteCategory(string Name, int Id)
        {
            var categories = await _categoryService.GetAllCategoriesAsync();

            if ( Name != null )
            {
                var newCategory = new CategoryDTO
                {
                    Name = Name
                };
                await _categoryService.AddCategoryAsync(newCategory);
            }
            if (Id != 0)
                await _categoryService.DeleteCategoryAsync(Id);

            return RedirectToAction("EditDeleteCategory"); 
        }
      
          
        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {            
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound(); 
            }
            return View(product); 
        }

       
        [HttpPost]
        public async Task<IActionResult> EditProduct(ProductDTO productDto)
        {
            if (!ModelState.IsValid)
            {
                return View(productDto);
            }
                        
            await _productService.UpdateProductAsync(productDto);
        
            return RedirectToAction("MainPage1");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int id)
        {            
            await _productService.DeleteProductAsync(id);                       
            return RedirectToAction("MainPage1");
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            var subcategories = await _subcategoryService.GetAllSubcategoriesAsync();
                        
            ViewBag.Subcategories = subcategories;
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductDTO productDto)
        {
            
            await _productService.AddProductAsync(productDto);
            return RedirectToAction("MainPage1");
        }

        [HttpGet]
        public async Task<IActionResult> EditDeleteSubcategory()
        {
            var category = await _categoryService.GetAllCategoriesAsync();
            var subcategories = await _subcategoryService.GetAllSubcategoriesAsync();      
            ViewBag.Categories = category;
            return View(subcategories); 
        }

        [HttpPost]
        public async Task<IActionResult> EditDeleteSubcategory(string Name, int CategoryId, int Id )
        {
            var category = await _categoryService.GetAllCategoriesAsync();
            var subcategories = await _subcategoryService.GetAllSubcategoriesAsync();
            if (Name != null && CategoryId != 0)
            {
                var newSubcategory = new SubcategoryDTO
                {
                    Name = Name,
                    CategoryId = CategoryId
                };

                await _subcategoryService.AddSubcategoryAsync(newSubcategory);
            }
            if (Id != 0)
                await _subcategoryService.DeleteSubcategoryAsync(Id);

            ViewBag.Categories = category;
            return RedirectToAction("EditDeleteSubcategory");
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubcategory(string Name, int CategoryId)
        {
            var newSubcategory = new SubcategoryDTO
            {
                Name = Name,
                CategoryId = CategoryId
            };

            await _subcategoryService.AddSubcategoryAsync(newSubcategory);
            return RedirectToAction("EditDeleteSubcategory"); 
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSubcategory(int id)
        {
            await _subcategoryService.DeleteSubcategoryAsync(id);
            return RedirectToAction("EditDeleteSubcategory"); 
        }


    }

}

