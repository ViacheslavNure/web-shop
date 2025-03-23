using Microsoft.EntityFrameworkCore;
using WebShop.Core.Helpers;
using WebShop.Core.Interfaces;
using WebShop.Core.Models.Cart;
using WebShop.Core.Models.Order;
using WebShop.Sql;
using WebShop.Sql.Models;

namespace WebShop.Core.Services
{
    public class OrderService(WebShopContext dbContext) : IOrderService
    {
        public async Task<long> CreateOrderAsync(string userId, CheckoutViewModel model, CancellationToken cancellationToken)
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

            var orderProducts = cart.ProductItems.Select(item => new AmountOfProducts
            {
                ProductId = item.ProductId,
                ProductsAmmount = item.ProductsAmmount,
                OrderId = Guid.NewGuid()
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

            dbContext.Order.Add(order);
            
            dbContext.AmountOfProducts.RemoveRange(cart.ProductItems);
            
            await dbContext.SaveChangesAsync(cancellationToken);

            return order.PublicOrderNumber;
        }

        public async Task<OrderViewModel> GetOrderDetailsAsync(long orderNumber, string staticFilesFolderPath, CancellationToken cancellationToken)
        {
            var order = await dbContext.Order
                .AsNoTracking()
                .Include(o => o.Products)
                    .ThenInclude(p => p.Product)
                .Include(o => o.PaymentDetails)
                .Include(o => o.DeliveryAddress)
                .FirstOrDefaultAsync(o => o.PublicOrderNumber == orderNumber);

            if (order == null)
            {
                throw new ArgumentException($"Order with number {orderNumber} not found");
            }

            return new OrderViewModel
            {
                Id = order.Id,
                CardCvv = order.PaymentDetails.CardCvv,
                CardExpiry = order.PaymentDetails.CardExpiry,
                CardNumber = order.PaymentDetails.CardNumber,
                City = order.DeliveryAddress?.City ?? string.Empty,
                DeliveryAddress = order.DeliveryAddress?.OrderDeliveryAddress ?? string.Empty,
                DeliveryMethod = Enum.Parse<DeliveryMethod>(order.DeliveryMethod),
                OrderComments = order.OrderComments,
                PostalCode = order.DeliveryAddress?.PostalCode ?? string.Empty,
                PublicOrderNumber = order.PublicOrderNumber,
                TotalPrice = order.TotalPrice,
                Items = order.Products.Select(p => new CartItemViewModel
                {
                    Brand = p.Product.Brand,
                    Model = p.Product.Model,
                    Price = p.Product.Price,
                    ImagePath = ImageHelper.GetImagePath(p.Product.ImageName, staticFilesFolderPath),
                    Quantity = p.ProductsAmmount
                }).ToList()
            };
        }
    }
}
