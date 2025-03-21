using WebShop.Core.Models.Product;

namespace WebShop.Core.Interfaces
{
    public interface ICartService
    {
        Task AddProductToCartAsync(Guid productId, string userId, CancellationToken cancellationToken);

        Task<CartViewModel> GetCartByUserIdAsync(string userId, string staticFilesFolderPath, CancellationToken cancellationToken);

        Task RemoveProductFromCartAsync(Guid productId, string userId, CancellationToken cancellationToken);

        Task UpdateCartItemQuantityAsync(UpdateCartItemRequest request, string userId, CancellationToken cancellationToken);
    }
}
