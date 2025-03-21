using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Sql.Models
{
    public class Cart
    {
        public Guid Id { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<CartProductItem> ProductItems { get; set; }
    }
}
