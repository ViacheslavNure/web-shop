using Microsoft.EntityFrameworkCore;
using WebShop.Core.Interfaces;
using WebShop.Core.Models.UIElements;
using WebShop.Core.Models.Product;
using WebShop.Sql;
using WebShop.Sql.Models;
using Microsoft.AspNetCore.Http;
using WebShop.Core.Helpers;

namespace WebShop.Core.Services
{
    public class ProductService(WebShopContext dbContext) : IProductService
    {
        public async Task CreateProductAsync(IFormFile productImage, string staticFolderPath, CreateProductViewModel productViewModel, CancellationToken cancellationToken)
        {
            var imageName = Guid.NewGuid().ToString() + Path.GetExtension(productImage.FileName);
            await ImageHelper.SaveImageAsync(productImage, imageName, staticFolderPath);

            var product = new Product
            {
                Brand = productViewModel.Brand,
                Model = productViewModel.Model,
                Description = productViewModel.Description,
                Price = productViewModel.Price,
                ImageName = imageName,
                ProductCategoryId = dbContext.ProductCategory.First(x => x.Name == productViewModel.Category).Id,
            };

            await dbContext.Product.AddAsync(product, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<NavBarItemViewModel>> GetAllProductCategories(CancellationToken cancellationToken)
        {
            return await dbContext.ProductCategory
                .AsNoTracking()
                .Select(c => new NavBarItemViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<ProductDetailsViewModel> GetProductDetails(Guid productId, string staticFilesFolderPath, CancellationToken cancellationToken)
        {
            var product = (await dbContext.Product
                .Include(p => p.Features)
                    .ThenInclude(p => p.FeatureCategory)
                .Include(p => p.ProductCategory)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == productId, cancellationToken))
                ?? throw new ArgumentOutOfRangeException($"Product with the Id {productId} was not found.");

            var categorizedFeatures = product.Features.GroupBy(f => f.FeatureCategory.Name)
                .Select(f => new ProductFeaturesCategoryViewModel
                {
                    Name = f.Key,
                    Features = f.Select(f => new ProductFeatureViewModel
                    {
                        Name = f.Name,
                        Value = f.Value
                    })
                });

            return new ProductDetailsViewModel
            {
                Id = product.Id,
                Brand = product.Brand,
                Model = product.Model,
                Description = product.Description,
                Price = product.Price,
                ImagePath = ImageHelper.GetImagePath(product.ImageName, staticFilesFolderPath),
                LikesCount = product.LikesCount,
                CategorizedFeatures = categorizedFeatures,
                ProductCategories = await dbContext.ProductCategory
                .AsNoTracking()
                .Select(c => new NavBarItemViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    RedirectToController = "Products",
                    RedirectToAction = "GridView",
                })
                .ToListAsync(cancellationToken)
            };
        }

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

            if (selectedProductsCategoryId == default)
            {
                selectedProductsCategoryId = categoriesQuery.First().Id;
            }

            var productsQuery = dbContext.Product
                .AsNoTracking()
                .Where(p => p.ProductCategoryId == selectedProductsCategoryId);

            var allBrands = await productsQuery.Select(p => p.Brand).ToListAsync();

            if (filters is not null)
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
            if (filters.MaxPrice.HasValue)
            {
                products = products.Where(p => p.Price <= filters.MaxPrice);
            }

            if (filters.MinPrice.HasValue)
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
