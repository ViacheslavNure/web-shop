using System.ComponentModel.DataAnnotations;

namespace WebShop.Sql.Models
{
    public class PaymentDetails
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        [MaxLength(20)]
        public string CardNumber { get; set; }

        [MaxLength(5)]
        public string CardExpiry { get; set; }

        [MaxLength(3)]
        public string CardCvv { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
