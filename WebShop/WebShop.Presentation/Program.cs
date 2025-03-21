using WebShop.Presentation.Extensions;
using Microsoft.AspNetCore.Identity;
using WebShop.Sql.Models;
using WebShop.Sql;

namespace WebShop.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.ConfigureDatabase(builder.Configuration);
            builder.Services.ConfigureServices();

            // Настройка аутентификации
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Authorization/LoginPage";
                options.LogoutPath = "/Authorization/LogoutUser";
                options.AccessDeniedPath = "/Authorization/AccessDenied";
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(7);
            });

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Products}/{action=GridView}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
