using System.Net.Http;
using System.Text.RegularExpressions;

namespace WebShop.DataGenerator
{
    public class ImageDownloader
    {
        private readonly HttpClient _httpClient;
        private readonly string _outputPath;

        public ImageDownloader(string outputPath)
        {
            _httpClient = new HttpClient();
            _outputPath = outputPath;
        }

        public async Task DownloadImagesAsync()
        {
            var products = new[]
            {
                // Велосипеди
                "girskiy-pro-x1",
                "miskiy-cruiser-2024",
                "elektrychniy-e-bike",
                "bmx-freestyle",
                "shoseyniy-master",
                "girskiy-trail-expert",
                "gibrydniy-urban",
                "dytiachiy-sport",
                "turystychniy-explorer",
                "fitness-velosyped-active",

                // Запчастини
                "galmovi-kolodky-shimano",
                "lantsiuh-kmc-x10",
                "kamery-continental",
                "pedali-wellgo",
                "rulova-kolonka-fsa",
                "vylka-rockshox",
                "kolesa-mavic",
                "manetky-sram",
                "kaseta-shimano",
                "galmova-avid",

                // Аксесуари
                "velosypedniy-likhtar",
                "velokompyuter",
                "velosypedniy-zamok",
                "velosypedniy-nasos",
                "velosypedniy-ryukzak",
                "velosypedniy-bazhnyk",
                "velosypedniy-koshik",
                "velosypedniy-dzvonok",
                "velosypedniy-shchytok",
                "velosypedniy-flazhok",

                // Електроніка
                "gps-treker",
                "velosypedna-kamera",
                "elektrychniy-nasos",
                "velosypedniy-likhtar-z-datchykom",
                "velosypedniy-dynamik",
                "velosypedniy-radar",
                "velosypedniy-signalniy-mayak",
                "velosypedniy-termometr",
                "velosypedniy-takhometr",
                "velosypedniy-kompas",

                // Екіпірування
                "velosypedniy-sholom",
                "velosypedni-rukavychky",
                "velosypedni-okulyary",
                "velosypedni-nakolinyky",
                "velosypedna-kurtka",
                "velosypedni-shkarpetky",
                "velosypedni-shorty",
                "velosypedni-cherevyky",
                "velosypedniy-zhilet",
                "velosypedni-nakolotnyky",

                // Інструменти
                "nabir-shestyhrannykiv",
                "multytul",
                "nabir-dlya-remontu-kamer",
                "nabir-tortsevykh-klyuchiv",
                "velosypedniy-stend",
                "velosypedniy-nasos",
                "nabir-vidvertok",
                "velosypedniy-klyuch",
                "nabir-dlya-chystky",
                "velosypedniy-stanok"
            };

            foreach (var product in products)
            {
                try
                {
                    var searchQuery = product.Replace("-", " ");
                    var imageUrl = await GetImageUrlAsync(searchQuery);
                    if (!string.IsNullOrEmpty(imageUrl))
                    {
                        await DownloadImageAsync(imageUrl, $"{product}.jpg");
                        Console.WriteLine($"Скачано изображение для {product}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при скачивании изображения для {product}: {ex.Message}");
                }
            }
        }

        private async Task<string> GetImageUrlAsync(string searchQuery)
        {
            try
            {
                var response = await _httpClient.GetStringAsync($"https://www.google.com/search?q={Uri.EscapeDataString(searchQuery)}+велосипед&tbm=isch");
                var matches = Regex.Matches(response, "\"ou\":\"([^\"]+)\"");
                foreach (Match match in matches)
                {
                    var url = match.Groups[1].Value;
                    if (url.EndsWith(".jpg") || url.EndsWith(".png") || url.EndsWith(".jpeg"))
                    {
                        return url;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при поиске изображения: {ex.Message}");
            }
            return null;
        }

        private async Task DownloadImageAsync(string imageUrl, string fileName)
        {
            try
            {
                var response = await _httpClient.GetByteArrayAsync(imageUrl);
                await File.WriteAllBytesAsync(Path.Combine(_outputPath, fileName), response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при скачивании изображения {fileName}: {ex.Message}");
            }
        }
    }
} 