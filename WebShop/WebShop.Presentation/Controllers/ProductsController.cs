using Microsoft.AspNetCore.Mvc;
using WebShop.Core.Interfaces;
using WebShop.Core.Models;

namespace WebShop.Presentation.Controllers
{
    //[Authorize]
    public class ProductsController(IWebHostEnvironment webHostEnvironment, IProductService productService) : Controller
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
        public IActionResult ProductById(Guid id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProductToCart(Guid productId)
        {
            return View();
        }
    }
}
