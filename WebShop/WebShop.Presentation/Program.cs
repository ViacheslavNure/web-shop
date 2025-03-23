using WebShop.Presentation.Extensions;
using Microsoft.AspNetCore.Identity;
using WebShop.Sql.Models;
using WebShop.Sql;

namespace WebShop.Presentation
{
    public class Program
    {
        public static async Task Main(string[] args)
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

            using var scope = app.Services.CreateScope();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

            if (await userManager.FindByNameAsync("admin") == null)
            {
                var user = new User
                {
                    UserName = "admin",
                    Email = "admin@gm.com",
                    PhoneNumber = "0000000000",
                };

                await userManager.CreateAsync(user, "Admin123!");
            }

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var roles = new[] { "Admin", "User" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var adminUser = await userManager.FindByNameAsync("admin");

            await userManager.AddToRoleAsync(adminUser!, "Admin");

            app.Run();
        }
    }
}
