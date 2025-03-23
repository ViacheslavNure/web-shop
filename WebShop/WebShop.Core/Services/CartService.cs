using Microsoft.EntityFrameworkCore;
using WebShop.Core.Interfaces;
using WebShop.Core.Models.Cart;
using WebShop.Core.Models.Product;
using WebShop.Presentation.Helpers;
using WebShop.Sql;
using WebShop.Sql.Models;

namespace WebShop.Core.Services
{
    public class CartService(WebShopContext dbContext) : ICartService
    {
        public async Task AddProductToCartAsync(Guid productId, string userId, CancellationToken cancellationToken)
        {
            var cart = await dbContext.Cart
                    .Include(c => c.ProductItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                throw new ArgumentException($"Cart for the user with ID {userId} not found");
            }

            var existingCartItem = cart.ProductItems
                .FirstOrDefault(item => item.ProductId == productId);

            if (existingCartItem != null)
            {
                existingCartItem.ProductsAmmount += 1;
            }
            else
            {
                cart.ProductItems.Add(new AmountOfProducts
                {
                    ProductId = productId,
                    ProductsAmmount = 1,
                    CartId = cart.Id,
                });
            }

            await dbContext.SaveChangesAsync(cancellationToken);

        }

        public async Task<CartViewModel> GetCartByUserIdAsync(string userId, string staticFilesFolderPath, CancellationToken cancellationToken)
        {
            var cart = await dbContext.Cart
                .AsNoTracking()
                .Include(c => c.ProductItems)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId, cancellationToken);

            if (cart is null)
            {
                throw new ArgumentException($"Cart for the user with ID {userId} not found");
            }

            var totalPrice = cart.ProductItems.Sum(p => p.Product.Price * p.ProductsAmmount);

            return new CartViewModel
            {
                TotalPrice = totalPrice,
                TotalItems = cart.ProductItems.Sum(p => p.ProductsAmmount),
                Items = cart.ProductItems.Select(p => new CartItemViewModel
                {
                    Id = p.Product.Id,
                    Brand = p.Product.Brand,
                    Model = p.Product.Model,
                    Price = p.Product.Price,
                    ImagePath = ImageHelper.GetImagePath(p.Product.ImageName, staticFilesFolderPath),
                    Quantity = p.ProductsAmmount,
                }).ToList()
            };
        }

        public async Task RemoveProductFromCartAsync(Guid productId, string userId, CancellationToken cancellationToken)
        {
            var cart = await dbContext.Cart
                .Include(c => c.ProductItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                throw new ArgumentException($"Cart for the user with ID {userId} not found");
            }

            var productItemToRemove = cart.ProductItems
                .FirstOrDefault(item => item.ProductId == productId);

            if(productItemToRemove is null)
            {
                throw new ArgumentException($"There is no product in the cart with product Id {productId}.");
            }

            cart.ProductItems.Remove(productItemToRemove);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateCartItemQuantityAsync(UpdateCartItemRequest request, string userId, CancellationToken cancellationToken)
        {
            var cart = await dbContext.Cart
                .Include(c => c.ProductItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                throw new ArgumentException($"Cart for the user with ID {userId} not found");
            }

            var product = cart.ProductItems
                .FirstOrDefault(item => item.ProductId == request.ProductId)
                ?? throw new ArgumentException($"Product with Id {request.ProductId} was not found in the cart.");

            product.ProductsAmmount = request.Quantity;

            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
