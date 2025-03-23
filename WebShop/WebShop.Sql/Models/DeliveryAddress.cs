using System.ComponentModel.DataAnnotations;

namespace WebShop.Sql.Models
{
    public class DeliveryAddress
    {
        [MaxLength(50)]
        public string? City { get; set; }

        [MaxLength(10)]
        public string? PostalCode { get; set; }

        [MaxLength(200)]
        public string? OrderDeliveryAddress { get; set; }
    }
}
