using Microsoft.EntityFrameworkCore;
using WebShop.Sql;
using WebShop.Sql.Models;

namespace WebShop.DataGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var options = new DbContextOptionsBuilder<WebShopContext>()
                .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WebShopDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False")
                .Options;

            using (var context = new WebShopContext(options))
            {
                //GenerateProductCategories(context);
                //GenerateFeatureCategories(context);
                //GenerateProducts(context);
                GenerateFeatures(context);

                Console.WriteLine("All data is generated.");
            }
        }

        static void GenerateFeatureCategories(WebShopContext context)
        {
            var categories = new List<FeatureCategory>();

            for (int i = 1; i <= 5; i++)
            {
                categories.Add(new FeatureCategory
                {
                    Id = Guid.NewGuid(),
                    Name = $"Category{i}"
                });
            }

            context.FearureCategory.AddRange(categories);
            context.SaveChanges();

            Console.WriteLine("Created (FeatureCategories).");
        }

        static void GenerateProducts(WebShopContext context)
        {
            var products = new List<Product>();

            var categories = context.ProductCategory.ToArray();

            for (int i = 1; i <= 40; i++)
            {
                products.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Brand = $"Brand{i}",
                    Model = $"Model{i}",
                    ImageName = $"Image{i}.jpg",
                    Description = $"Description for product {i}",
                    Price = new Random().Next(1, 1000),
                    LikesCount = new Random().Next(0, 500),
                    ProductCategoryId = categories[new Random().Next(0, categories.Length-1)].Id,
                });
            }

            context.Product.AddRange(products);
            context.SaveChanges();

            Console.WriteLine("Created (Products).");
        }

        static void GenerateFeatures(WebShopContext context)
        {
            var random = new Random();
            var features = new List<Feature>();

            var products = context.Product.ToList();
            var categories = context.FearureCategory.ToList();

            for (int i = 1; i <= 10; i++)
            {
                features.Add(new Feature
                {
                    Id = Guid.NewGuid(),
                    Name = $"Feature{i}",
                    Value = $"Value{i}",
                    FeatureCategoryId = categories[random.Next(categories.Count)].Id,
                    ProductId = products.First(x=>x.Brand == "Brand13").Id
                });
            }

            context.Feature.AddRange(features);
            context.SaveChanges();

            Console.WriteLine("Created (Features).");
        }

        static void GenerateProductCategories(WebShopContext context)
        {
            var random = new Random();
            var categories = new List<ProductCategory>();

            for (int i = 1; i <= 5; i++)
            {
                categories.Add(new ProductCategory
                {
                    Id = Guid.NewGuid(),
                    Name = $"Category{i}",
                });
            }

            context.ProductCategory.AddRange(categories);
            context.SaveChanges();

            Console.WriteLine("Created (ProductCategory).");
        }
    }
}
