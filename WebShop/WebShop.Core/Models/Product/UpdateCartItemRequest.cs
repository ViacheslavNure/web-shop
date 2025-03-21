namespace WebShop.Core.Models.Product
{
    public class UpdateCartItemRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
} 