using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebShop.Core.Interfaces;
using WebShop.Core.Services;
using WebShop.Sql;
using WebShop.Sql.Models;

namespace WebShop.Presentation.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlServerConnection");

            services.AddDbContext<WebShopContext>(option => option.UseSqlServer(connectionString));

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
            })
            .AddEntityFrameworkStores<WebShopContext>()
            .AddDefaultTokenProviders();

            return services;
        }

        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
