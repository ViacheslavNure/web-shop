using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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

        public DbSet<AmountOfProducts> AmountOfProducts { get; set; }

        public DbSet<Cart> Cart { get; set; }

        public DbSet<Order> Order { get; set; }

        public DbSet<PaymentDetails> PaymentDetails { get; set; }

        public WebShopContext()
        { }

        public WebShopContext(DbContextOptions<WebShopContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasSequence<long>("OrderNumbers")
                .StartsAt(1)
                .IncrementsBy(1);

            var roles = Enum.GetValues<UserRole>().Select(r => new Role
            {
                Id = ((int)r).ToString(),
                Name = r.ToString()
            });

            modelBuilder.Entity<Role>().HasData(roles);

            modelBuilder.Entity<Order>()
                .OwnsOne(o => o.DeliveryAddress);

            modelBuilder.Entity<Order>()
                .Property(o => o.PublicOrderNumber)
                .HasDefaultValueSql("NEXT VALUE FOR OrderNumbers");

            modelBuilder.Entity<Order>()
                .HasOne(o => o.PaymentDetails)
                .WithMany(pd => pd.Orders)
                .HasForeignKey(o => o.PaymentDetailsId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PaymentDetails>()
                .HasMany(pd => pd.Orders)
                .WithOne(o => o.PaymentDetails)
                .HasForeignKey(o => o.PaymentDetailsId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
