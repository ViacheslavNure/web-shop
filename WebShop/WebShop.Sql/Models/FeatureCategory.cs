using System.ComponentModel.DataAnnotations;

namespace WebShop.Sql.Models
{
    public class FeatureCategory
    {
        public Guid Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
    }
}
