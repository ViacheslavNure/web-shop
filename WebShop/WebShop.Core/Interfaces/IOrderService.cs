using WebShop.Core.Models.Order;

namespace WebShop.Core.Interfaces
{
    public interface IOrderService
    {
        Task<long> CreateOrderAsync(string userId, string staticFilesFolderPath, CheckoutViewModel model, CancellationToken cancellationToken);
    }
}
