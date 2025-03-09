using Microsoft.EntityFrameworkCore;
using WebShop.Core.Interfaces;
using WebShop.Core.Models;
using WebShop.Presentation.Helpers;
using WebShop.Presentation.Models;
using WebShop.Sql;
using WebShop.Sql.Models;

namespace WebShop.Core.Services
{
    public class ProductService(WebShopContext dbContext) : IProductService
    {
        public async Task<ProductsGridViewModel> GetProductsCollection(
            Guid selectedProductsCategoryId,
            int pageNumber,
            int amountPerPage,
            string staticFilesFolderPath,
            ProductGridFilterViewModel filters,
            CancellationToken cancellationToken)
        {
            var itemsToSkip = (pageNumber - 1) * amountPerPage;

            var categoriesQuery = await dbContext.ProductCategory
                .Select(c => new NavBarItemViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    RedirectToController = "Products",
                    RedirectToAction = "GridView",
                })
                .ToListAsync();

            if(selectedProductsCategoryId == default)
            {
                selectedProductsCategoryId = categoriesQuery.First().Id;
            }

            var productsQuery = dbContext.Product
                .AsNoTracking()
                .Where(p => p.ProductCategoryId == selectedProductsCategoryId);

            var allBrands = await productsQuery.Select(p => p.Brand).ToListAsync();

            if(filters is not null)
            {
                productsQuery = ApplyProductFilters(productsQuery, filters);
            }

            var products = await productsQuery
                .Skip(itemsToSkip)
                .Take(amountPerPage)
                .Select(p => new ProductCardViewModel
                {
                    Id = p.Id,
                    Brand = p.Brand,
                    Price = p.Price,
                    ImagePath = ImageHelper.GetImagePath(p.ImageName, staticFilesFolderPath),
                    Model = p.Model,
                    Description = p.Description,
                    LikesCount = p.LikesCount
                })
                .ToListAsync();

            var totalProductsCount = await dbContext.Product
                .AsNoTracking()
                .Where(p => p.ProductCategoryId == selectedProductsCategoryId)
                .CountAsync();

            return new ProductsGridViewModel
            {
                Products = products,
                AmountPerPage = amountPerPage,
                CurrentPageNumber = pageNumber,
                TotalAmount = totalProductsCount,
                ProductCategories = categoriesQuery,
                SelectedProductCategoryId = selectedProductsCategoryId,
                AllBrands = allBrands,
            };
        }

        private IQueryable<Product> ApplyProductFilters(IQueryable<Product> products, ProductGridFilterViewModel filters)
        {
            if(filters.MaxPrice.HasValue)
            {
                products = products.Where(p => p.Price <= filters.MaxPrice);
            }

            if(filters.MinPrice.HasValue)
            {
                products = products.Where(p => p.Price >= filters.MinPrice);
            }

            if (filters.Brands is not null && filters.Brands.Any())
            {
                products = products.Where(p => filters.Brands.Contains(p.Brand));
            }

            return products;
        }
    }
}
