using WebShop.Core.Models;
using WebShop.Presentation.Models;

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
    }
}
