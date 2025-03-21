using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using WebShop.Sql.Models;
using WebShop.Sql.Models.Enums;

namespace WebShop.Sql
{
    public class WebShopContext : IdentityDbContext<User>
    {
        public DbSet<Role> Role { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<Feature> Feature { get; set; }

        public DbSet<FeatureCategory> FearureCategory { get; set; }

        public DbSet<ProductCategory> ProductCategory { get; set; }

        public DbSet<CartProductItem> CartProductItem { get; set; }

        public DbSet<Cart> Cart { get; set; }

        public WebShopContext()
        { }

        public WebShopContext(DbContextOptions<WebShopContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var roles = Enum.GetValues<UserRole>().Select(r => new Role
            {
                Id = ((int)r).ToString(),
                Name = r.ToString()
            });

            modelBuilder.Entity<Role>().HasData(roles);
        }
    }
}
