namespace WebShop.Core.Models.Product
{
    public class ProductGridFilterViewModel
    {
        public decimal? MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }

        public List<string> Brands { get; set; } = [];

        public bool IsEmpty() => !MinPrice.HasValue && !MaxPrice.HasValue && !Brands.Any();
    }
}
