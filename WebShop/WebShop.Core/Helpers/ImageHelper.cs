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
    }
}
