using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebShop.Core.Interfaces;
using WebShop.Core.Models.Product;

namespace WebShop.Presentation.Controllers
{
    public class ProductsController(
        IWebHostEnvironment webHostEnvironment,
        IProductService productService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GridView(
            Guid selectedProductsCategoryId,
            int pageNumber = 1,
            int amountPerpage = 18,
            ProductGridFilterViewModel filters = null!,
            CancellationToken cancellationToken = default)
        {
            if(selectedProductsCategoryId == default)
            {
                selectedProductsCategoryId = (Guid)(TempData["SelectedCategoryId"] ?? Guid.Empty);
            }

            var products = await productService.GetProductsCollection(
                selectedProductsCategoryId,
                pageNumber,
                amountPerpage,
                webHostEnvironment.WebRootPath,
                filters,
                cancellationToken);

            ViewData["ShowNavbar"] = true;
            ViewData["Brands"] = products.AllBrands;
            TempData["SelectedCategoryId"] = selectedProductsCategoryId;
            ViewData["ShowSideBar"] = true;
            ViewData["ProductCategories"] = products.ProductCategories;

            return View((products, filters));
        }

        [HttpPost]
        public IActionResult FilterGridView(ProductGridFilterViewModel filters)
        {
            return RedirectToAction(nameof(GridView), filters);
        }

        [HttpPost]
        public IActionResult ClearFilters()
        {
            return RedirectToAction(nameof(GridView));
        }

        [HttpGet]
        public async Task<IActionResult> ProductById(Guid id, CancellationToken cancellationToken)
        {
            var product = await productService.GetProductDetails(id, webHostEnvironment.WebRootPath, cancellationToken);
            ViewData["ShowNavbar"] = true;
            ViewData["ProductCategories"] = product.ProductCategories;

            return View(product);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateProductPage(CancellationToken cancellationToken)
        {
            var productCategories = await productService.GetAllProductCategories(cancellationToken);

            ViewData["ProductCategories"] = productCategories;
            ViewData["ShowNavbar"] = true;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateProduct(CreateProductViewModel productViewModel, IFormFile productImage, CancellationToken cancellationToken)
        {
            await productService.CreateProductAsync(productImage, webHostEnvironment.WebRootPath, productViewModel, cancellationToken);

            return RedirectToAction(nameof(GridView));
        }
    }
}
