using WebShop.Core.Models.Product;
using WebShop.Core.Models.UIElements;
using Microsoft.AspNetCore.Http;

namespace WebShop.Core.Interfaces
{
    public interface IProductService
    {
        Task<ProductsGridViewModel> GetProductsCollection(
            Guid selectedProductsCategoryId,
            int pageNumber,
            int amountPerPage,
            string staticFilesFolderPath,
            ProductGridFilterViewModel filters,
            CancellationToken cancellationToken);

        Task<ProductDetailsViewModel> GetProductDetails(
            Guid productId,
            string staticFilesFolderPath,
            CancellationToken cancellationToken);

        Task<IEnumerable<NavBarItemViewModel>> GetAllProductCategories(CancellationToken cancellationToken);

        Task CreateProductAsync(
            IFormFile productImage,
            string staticFolderPath,
            CreateProductViewModel productViewModel,
            CancellationToken cancellationToken);
    }
}
