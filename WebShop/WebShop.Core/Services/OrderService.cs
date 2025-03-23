using Microsoft.EntityFrameworkCore;
using WebShop.Core.Interfaces;
using WebShop.Core.Models.Order;
using WebShop.Sql;
using WebShop.Sql.Models;

namespace WebShop.Core.Services
{
    public class OrderService(WebShopContext dbContext, ICartService cartService) : IOrderService
    {
        public async Task<long> CreateOrderAsync(string userId, string staticFilesFolderPath, CheckoutViewModel model, CancellationToken cancellationToken)
        {
            var cart = await dbContext.Cart
                .AsNoTracking()
                    .Include(c => c.ProductItems)
                        .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                throw new ArgumentException($"Cart for the user with ID {userId} not found");
            }

            var paymentDetails = dbContext.PaymentDetails.Where(pd => pd.UserId == userId)
                .FirstOrDefault(p =>
                p.CardCvv == model.CardCvv
                && p.CardExpiry == model.CardExpiry
                && p.CardNumber == model.CardNumber) ?? new PaymentDetails
                {
                    UserId = userId,
                    CardCvv = model.CardCvv,
                    CardExpiry = model.CardExpiry,
                    CardNumber = model.CardNumber
                };

            // Создаем новые CartProductItem для заказа
            var orderProducts = cart.ProductItems.Select(item => new AmountOfProducts
            {
                ProductId = item.ProductId,
                ProductsAmmount = item.ProductsAmmount,
                OrderId = Guid.NewGuid() // Временный ID, будет заменен при сохранении
            }).ToList();

            var order = new Order
            {
                UserId = userId,
                PaymentDetails = paymentDetails,
                CreationDate = DateTime.Now,
                DeliveryAddress = new DeliveryAddress
                {
                    City = model.City,
                    OrderDeliveryAddress = model.DeliveryAddress,
                    PostalCode = model.PostalCode,
                },
                DeliveryMethod = model.DeliveryMethod.ToString(),
                OrderComments = model.OrderComments ?? string.Empty,
                Products = orderProducts,
                TotalPrice = cart.ProductItems.Sum(p => p.Product.Price * p.ProductsAmmount),
            };

            // Добавляем заказ
            dbContext.Order.Add(order);
            
            // Удаляем товары из корзины
            dbContext.AmountOfProducts.RemoveRange(cart.ProductItems);
            
            await dbContext.SaveChangesAsync(cancellationToken);

            return order.PublicOrderNumber;
        }
    }
}
