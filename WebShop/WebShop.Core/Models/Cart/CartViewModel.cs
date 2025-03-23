namespace WebShop.Core.Models.Cart
{
    public class CartViewModel
    {
        public List<CartItemViewModel> Items { get; set; } = new();

        public int TotalItems { get; set; }

        public decimal TotalPrice { get; set; }
    }
} 