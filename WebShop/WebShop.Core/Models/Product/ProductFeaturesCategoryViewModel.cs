namespace WebShop.Core.Models.Product
{
    public class ProductFeaturesCategoryViewModel
    {
        public string Name { get; set; }

        public IEnumerable<ProductFeatureViewModel> Features { get; set; }
    }
}
