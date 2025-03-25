using Microsoft.EntityFrameworkCore;
using WebShop.Sql;
using WebShop.Sql.Models;

namespace WebShop.DataGenerator
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WebShopContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=WebShopDb;Trusted_Connection=True;MultipleActiveResultSets=true");

            using var context = new WebShopContext(optionsBuilder.Options);

            // Очистка базы данных
            //Console.WriteLine("Очистка базы данных...");
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();

            // Создание категорий
            var categories = new[]
            {
                "Велосипеди",
                "Запчастини",
                "Аксесуари",
                "Електроніка",
                "Екіпірування",
                "Інструменти"
            };

            var productCategories = new List<ProductCategory>();
            foreach (var categoryName in categories)
            {
                productCategories.Add(new ProductCategory { Name = categoryName });
            }

            context.ProductCategory.AddRange(productCategories);
            await context.SaveChangesAsync();

            return;
            // Генерация товаров для каждой категории
            var random = new Random();

            // Велосипеды
            var bicycles = new[]
            {
                ("Гірський Pro X1", "Гірський велосипед для початківців. Алюмінієва рама, 21 передача, дискові гальма. Ідеально підходить для катань по пересіченій місцевості.", 15999),
                ("Міський Cruiser 2024", "Міський велосипед з комфортною посадкою. Сталева рама, 7 передач, захист ланцюга. Чудово підходить для щоденних поїздок містом.", 12999),
                ("Електричний E-Bike", "Електричний велосипед з потужним мотором 500W. Запас ходу до 80 км, LCD дисплей, 7 передач. Ідеальний вибір для міських поїздок.", 45999),
                ("BMX Freestyle", "BMX велосипед для трюків. Міцна сталева рама, 20-дюймові колеса, пеги. Підходить для виконання трюків та стрибків.", 18999),
                ("Шосейний Master", "Шосейний велосипед для швидкісної їзди. Карбонова рама, 22 передачі, полегшені колеса. Професійний вибір для гонок.", 69999),
                ("Гірський Trail Expert", "Професійний гірський велосипед. Карбонова рама, 12 передач, підвіска 120мм. Для екстремального катанья.", 79999),
                ("Гібридний Urban", "Гібридний велосипед для міста. Алюмінієва рама, 18 передач, підвіска 60мм. Комфортна посадка.", 22999),
                ("Дитячий Sport", "Дитячий велосипед для початківців. Сталева рама, 6 передач, допоміжні колеса в комплекті.", 8999),
                ("Туристичний Explorer", "Туристичний велосипед з багажником. Сталева рама, 24 передачі, гальма V-brake.", 18999),
                ("Фітнес-велосипед Active", "Фітнес-велосипед для тренувань. Алюмінієва рама, 21 передача, легкі колеса.", 15999)
            };

            // Запчасти
            var parts = new[]
            {
                ("Гальмові колодки Shimano", "Якісні гальмові колодки для дискових гальм. Сумісні з більшістю моделей Shimano.", 1299),
                ("Ланцюг KMC X10", "10-швидкісний ланцюг з антикорозійним покриттям. Довговічний та надійний.", 899),
                ("Камери Continental", "Набір з 2 камер для гірських велосипедів. Розмір 26x2.0-2.4", 799),
                ("Педалі Wellgo", "Платформні педалі з шипами. Алюмінієвий корпус, сталеві шипи.", 1499),
                ("Рульова колонка FSA", "Головна труба з підшипниками. Сумісна з більшістю рам.", 1999),
                ("Вилка RockShox", "Амортизаційна вилка з ходом 100мм. Налаштування жорсткості та відбою.", 12999),
                ("Колеса Mavic", "Комплект коліс для гірського велосипеда. Ободи з алюмінію, 32 спиці.", 8999),
                ("Манетки SRAM", "12-швидкісні манетки для гірського велосипеда. Ергономічний дизайн.", 3999),
                ("Кассета Shimano", "10-швидкісна касета. Діапазон 11-42 зубці.", 2499),
                ("Тормоза Avid", "Гідравлічні дискові гальма. Ротор 180мм, 2 поршні.", 2999)
            };

            // Аксессуары
            var accessories = new[]
            {
                ("Велосипедний ліхтар", "LED ліхтар з яскравістю 1000 люмен. Режими роботи: постійний, миготливий, SOS.", 1299),
                ("Велокомп'ютер", "Бездротовий велокомп'ютер з GPS. Вимірює швидкість, відстань, час у дорозі.", 2499),
                ("Велосипедний замок", "U-подібний замок з захистом від перекушування. Довжина 30 см.", 1999),
                ("Велосипедний насос", "Компактний насос з манометром. Максимальний тиск 8 бар.", 899),
                ("Велосипедний рюкзак", "Водонепроникний рюкзак об'ємом 20 літрів. З відбивачами.", 2499),
                ("Велосипедний багажник", "Задній багажник з кріпленням для сумок. Максимальне навантаження 25 кг.", 1999),
                ("Велосипедний кошик", "Передній кошик з кріпленням на руль. Об'єм 15 літрів.", 1499),
                ("Велосипедний дзвінок", "Гучний дзвінок з регулюванням тону. Алюмінієвий корпус.", 499),
                ("Велосипедний щиток", "Захисний щиток від бруду. Встановлюється на заднє колесо.", 799),
                ("Велосипедний флажок", "Світловідбиваючий флажок для дитячого велосипеда.", 299)
            };

            // Электроника
            var electronics = new[]
            {
                ("GPS-трекер", "Мініатюрний GPS-трекер для велосипеда. Працює до 30 днів від одного заряду.", 2999),
                ("Велосипедна камера", "Action-камера з роздільною здатністю 4K. Водонепроникний корпус.", 3999),
                ("Електричний насос", "Автоматичний насос з дисплеєм. Працює від прикурювача.", 1999),
                ("Велосипедний ліхтар з датчиком", "Розумний ліхтар з датчиком руху. Автоматично вмикається в темряві.", 1599),
                ("Велосипедний динамік", "Bluetooth-динамік з захистом від води. Кріпиться на кермо.", 2499),
                ("Велосипедний радар", "Радар для виявлення автомобілів позаду. Дальність 140 метрів.", 4999),
                ("Велосипедний сигнальний маяк", "Світловідбиваючий маяк з режимами блимання. USB-зарядка.", 999),
                ("Велосипедний термометр", "Цифровий термометр з вогнестійким корпусом.", 799),
                ("Велосипедний тахометр", "Бездротовий тахометр з пам'яттю на 100 годин.", 1499),
                ("Велосипедний компас", "Електронний компас з підсвіткою. Водонепроникний.", 699)
            };

            // Экипировка
            var equipment = new[]
            {
                ("Велосипедний шолом", "Легкий шолом з вентиляцією. Відповідає стандартам безпеки.", 1999),
                ("Велосипедні рукавички", "Рукавички з гелевими вставками. Захист від натирання.", 899),
                ("Велосипедні окуляри", "Захисні окуляри з UV-фільтром. Змінні лінзи в комплекті.", 1499),
                ("Велосипедні наколінники", "Захисні наколінники з м'якими вставками.", 1299),
                ("Велосипедна куртка", "Вітро- та водонепроникна куртка з відбивачами.", 2999),
                ("Велосипедні шкарпетки", "Спортивні шкарпетки з компресією. Вентиляційні зони.", 499),
                ("Велосипедні шорти", "Шорти з гелевою вставкою. Вентиляційні панелі.", 1499),
                ("Велосипедні черевики", "Спортивні черевики з жорсткою підошвою.", 3999),
                ("Велосипедний жилет", "Світловідбиваючий жилет з вентиляцією.", 799),
                ("Велосипедні налокотники", "Захисні налокотники з регулюванням.", 999)
            };

            // Инструменты
            var tools = new[]
            {
                ("Набір шестигранників", "Набір з 8 шестигранних ключів. Хром-ванадієва сталь.", 799),
                ("Мультитул", "Компактний мультитул з 16 функціями. Включає ключі, відвертки, ніж.", 1499),
                ("Набір для ремонту камер", "Комплект для ремонту проколів. Включає латки та клей.", 499),
                ("Набір торцевих ключів", "Набір з 10 торцевих ключів. Розміри від 8 до 17 мм.", 1999),
                ("Велосипедний стенд", "Складний стенд для ремонту. Максимальне навантаження 30 кг.", 2499),
                ("Велосипедний насос", "Напольний насос з манометром. Максимальний тиск 11 бар.", 1999),
                ("Набір відверток", "Набір з 6 відверток різних розмірів.", 899),
                ("Велосипедний ключ", "Універсальний ключ для ремонту. 15 функцій.", 1299),
                ("Набір для чистки", "Комплект для чищення ланцюга та трансмісії.", 699),
                ("Велосипедний станок", "Професійний станок для ремонту. Включає виправляч ободів.", 8999)
            };

            var products = new List<Product>();
            var categoriesDict = productCategories.ToDictionary(c => c.Name);

            // Добавление товаров по категориям
            foreach (var (name, description, price) in bicycles)
            {
                products.Add(new Product
                {
                    Brand = "BikePro",
                    Model = name,
                    Description = description,
                    Price = price,
                    ImageName = $"{name.ToLower().Replace(" ", "-")}.jpg",
                    ProductCategoryId = categoriesDict["Велосипеди"].Id,
                    LikesCount = random.Next(10, 100)
                });
            }

            foreach (var (name, description, price) in parts)
            {
                products.Add(new Product
                {
                    Brand = "BikeParts",
                    Model = name,
                    Description = description,
                    Price = price,
                    ImageName = $"{name.ToLower().Replace(" ", "-")}.jpg",
                    ProductCategoryId = categoriesDict["Запчастини"].Id,
                    LikesCount = random.Next(10, 100)
                });
            }

            foreach (var (name, description, price) in accessories)
            {
                products.Add(new Product
                {
                    Brand = "BikeAccessories",
                    Model = name,
                    Description = description,
                    Price = price,
                    ImageName = $"{name.ToLower().Replace(" ", "-")}.jpg",
                    ProductCategoryId = categoriesDict["Аксесуари"].Id,
                    LikesCount = random.Next(10, 100)
                });
            }

            foreach (var (name, description, price) in electronics)
            {
                products.Add(new Product
                {
                    Brand = "BikeTech",
                    Model = name,
                    Description = description,
                    Price = price,
                    ImageName = $"{name.ToLower().Replace(" ", "-")}.jpg",
                    ProductCategoryId = categoriesDict["Електроніка"].Id,
                    LikesCount = random.Next(10, 100)
                });
            }

            foreach (var (name, description, price) in equipment)
            {
                products.Add(new Product
                {
                    Brand = "BikeProtection",
                    Model = name,
                    Description = description,
                    Price = price,
                    ImageName = $"{name.ToLower().Replace(" ", "-")}.jpg",
                    ProductCategoryId = categoriesDict["Екіпірування"].Id,
                    LikesCount = random.Next(10, 100)
                });
            }

            foreach (var (name, description, price) in tools)
            {
                products.Add(new Product
                {
                    Brand = "BikeTools",
                    Model = name,
                    Description = description,
                    Price = price,
                    ImageName = $"{name.ToLower().Replace(" ", "-")}.jpg",
                    ProductCategoryId = categoriesDict["Інструменти"].Id,
                    LikesCount = random.Next(10, 100)
                });
            }

            context.Product.AddRange(products);
            await context.SaveChangesAsync();

            // Скачивание изображений
            Console.WriteLine("Скачивание изображений...");
            var imageDownloader = new ImageDownloader(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "WebShop.Presentation", "wwwroot", "images"));
            await imageDownloader.DownloadImagesAsync();

            Console.WriteLine("База данных успешно заполнена!");
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
