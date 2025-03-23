using System.ComponentModel.DataAnnotations;

namespace WebShop.Sql.Models
{
    public class Order
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public long PublicOrderNumber { get; set; }

        public string OrderComments { get; set; }

        [MaxLength(50)]
        public string DeliveryMethod { get; set; }

        public DeliveryAddress DeliveryAddress { get; set; }

        public virtual ICollection<AmountOfProducts> Products { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime CreationDate { get; set; }

        public Guid PaymentDetailsId { get; set; }

        public virtual PaymentDetails PaymentDetails { get; set; }
    }
}
