using WebShop.Core.Models.Order;

namespace WebShop.Core.Interfaces
{
    public interface IOrderService
    {
        Task<long> CreateOrderAsync(string userId, CheckoutViewModel model, CancellationToken cancellationToken);

        Task<OrderViewModel> GetOrderDetailsAsync(long orderNumber, string staticFilesFolderPath, CancellationToken cancellationToken);
    }
}
