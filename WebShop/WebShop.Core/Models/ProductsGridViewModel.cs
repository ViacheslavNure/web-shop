using WebShop.Core.Models;

namespace WebShop.Presentation.Models
{
    public class ProductsGridViewModel : PaginationModel
    {
        public Guid SelectedProductCategoryId { get; set; }

        public List<string> AllBrands { get; set; }

        public List<NavBarItemViewModel> ProductCategories { get; set; }

        public List<ProductCardViewModel> Products { get; set; }
    }
}
