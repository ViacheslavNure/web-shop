namespace WebShop.Core.Models.Product
{
    public class ProductCardViewModel
    {
        public Guid Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int LikesCount { get; set; }

        public string ImagePath { get; set; }
    }
}
