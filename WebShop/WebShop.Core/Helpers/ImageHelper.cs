using Microsoft.AspNetCore.Http;
using WebShop.Core;

namespace WebShop.Presentation.Helpers
{
    public static class ImageHelper
    {
        public static string GetImagePath(string imageName, string staticFilesFolderPath)
        {
            var imagePath = Path.Combine(Constants.ImagesFolderPath, imageName);

            var fullImagePath = Path.Combine(staticFilesFolderPath, imagePath);

            if (!File.Exists(fullImagePath))
            {
                imagePath = Constants.DefaultImageName;
            }

            return imagePath;
        }

        public static async Task SaveImageAsync(IFormFile image,string imageName, string staticFilesFolderPath)
        {
            var imagePath = Path.Combine(staticFilesFolderPath, Constants.ImagesFolderPath, imageName);
            
            using var fileStream = new FileStream(imagePath, FileMode.Create);

            await image.CopyToAsync(fileStream);
        }
    }
}
