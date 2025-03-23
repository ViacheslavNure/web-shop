using WebShop.Core.Models.Cart;

namespace WebShop.Core.Models.Order
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }

        public long PublicOrderNumber { get; set; }

        public string DeliveryAddress { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public DeliveryMethod DeliveryMethod { get; set; }

        public string CardNumber { get; set; }

        public string CardExpiry { get; set; }

        public string CardCvv { get; set; }

        public string? OrderComments { get; set; }

        public List<CartItemViewModel> Items { get; set; } = new();

        public decimal TotalPrice { get; set; }
    }
}
