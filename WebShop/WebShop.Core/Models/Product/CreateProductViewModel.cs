using System.Globalization;

namespace WebShop.Core.Models.Product
{
    public class CreateProductViewModel
    {
        public string Brand { get; set; }

        public string Model { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public IEnumerable<ProductFeaturesCategoryViewModel> Features { get; set; }
    }
}
