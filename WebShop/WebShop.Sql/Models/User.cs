using Microsoft.AspNetCore.Identity;

namespace WebShop.Sql.Models
{
    public class User : IdentityUser
    {
        public Guid CartId { get; set; }

        public virtual Cart Cart { get; set; }

        public virtual ICollection<PaymentDetails> PaymentDetails { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
