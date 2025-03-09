using System.ComponentModel.DataAnnotations;

namespace WebShop.Sql.Models
{
    public class ProductCategory
    {
        public Guid Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
