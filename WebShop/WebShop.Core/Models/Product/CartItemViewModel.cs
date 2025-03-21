namespace WebShop.Core.Models.Product
{
    public class CartItemViewModel
    {
        public Guid Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
} 