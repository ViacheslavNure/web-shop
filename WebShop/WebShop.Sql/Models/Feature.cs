using System.ComponentModel.DataAnnotations;

namespace WebShop.Sql.Models
{
    public class Feature
    {
        public Guid Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public string Value { get; set; }

        public Guid FeatureCategoryId { get; set; }

        public virtual FeatureCategory FeatureCategory { get; set; }

        public Guid ProductId { get; set; }

        public Product Product { get; set; }
    }
}
