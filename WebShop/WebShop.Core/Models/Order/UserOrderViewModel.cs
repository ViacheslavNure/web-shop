namespace WebShop.Core.Models.Order
{
    public class UserOrderViewModel
    {
        public long OrderNumber { get; set; }

        public DateTime OrderDate { get; set; }

        public string DeliveryAddress { get; set; }

        public DeliveryMethod DeliveryMethod { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
