using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebShop.Core.Interfaces;
using WebShop.Core.Models.Order;
using WebShop.Core.Models.User;
using WebShop.Sql;
using WebShop.Sql.Models;

namespace WebShop.Core.Services
{
    public class UserProfileService(WebShopContext dbContext, UserManager<User> userManager) : IUserProfileService
    {
        public async Task<UserProfileViewModel> GetUserProfileBuUserId(string userId, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(userId);

            var orders = await dbContext.Order
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.CreationDate)
                .Select(o => new UserOrderViewModel
                {
                    OrderNumber = o.PublicOrderNumber,
                    OrderDate = o.CreationDate,
                    DeliveryAddress = o.DeliveryAddress.OrderDeliveryAddress,
                    DeliveryMethod = Enum.Parse<DeliveryMethod>(o.DeliveryMethod, true),
                    TotalPrice = o.TotalPrice,
                })
                .ToListAsync();

            var totalOrders = await dbContext.Order
                .Where(o => o.UserId == userId)
                .CountAsync();

            var totalPrice = await dbContext.Order
                .Where(o => o.UserId == userId)
                .SumAsync(o => o.TotalPrice);

            var viewModel = new UserProfileViewModel
            {
                Email = user.Email,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                OrdersCount = totalOrders,
                TotalOrdersPrice = totalPrice,
                RecentOrders = orders
            };

            return viewModel;
        }
    }
}
