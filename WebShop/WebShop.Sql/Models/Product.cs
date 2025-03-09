using System.ComponentModel.DataAnnotations;

namespace WebShop.Sql.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        [MaxLength(50)]
        public string Brand { get; set; }

        [MaxLength(200)]
        public string Model { get; set; }

        public string ImageName { get; set; }

        public string Description { get; set; }

        [Range(0, (double)decimal.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int LikesCount { get; set; }

        public Guid ProductCategoryId { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }

        public virtual ICollection<Feature> Features { get; set; }
    }
}
