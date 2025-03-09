using WebShop.Presentation.Extensions;
using Microsoft.Extensions.DependencyInjection;

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

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

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
