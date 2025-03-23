using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebShop.Core.Interfaces;
using WebShop.Core.Models.Product;
using WebShop.Core.Services;
using WebShop.Sql.Models;

namespace WebShop.Presentation.Controllers
{
    [Authorize]
    public class CartController(
        IWebHostEnvironment webHostEnvironment,
        ICartService cartService,
        UserManager<User> userManager) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> CartPage(CancellationToken cancellationToken)
        {
            var cartViewModel = await cartService.GetCartByUserIdAsync(
                userManager.GetUserId(User)!,
                webHostEnvironment.WebRootPath,
                cancellationToken);

            ViewData["ShowNavbar"] = true;
            return View(cartViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddProductToCart([FromBody] AddToCartRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = userManager.GetUserId(User);

                await cartService.AddProductToCartAsync(request.ProductId, userId, cancellationToken);
                
                var cart = await cartService.GetCartByUserIdAsync(
                    userId!,
                    webHostEnvironment.WebRootPath,
                    cancellationToken);

                return Json(new { success = true, count = cart.TotalItems });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart([FromBody] RemoveFromCartRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = userManager.GetUserId(User);
                await cartService.RemoveProductFromCartAsync(request.ProductId, userId, cancellationToken);

                var updatedCart = await cartService.GetCartByUserIdAsync(
                userManager.GetUserId(User)!,
                webHostEnvironment.WebRootPath,
                cancellationToken);

                return Json(new
                {
                    success = true,
                    totalItems = updatedCart.TotalItems,
                    totalPrice = updatedCart.TotalPrice,
                    count = updatedCart.TotalItems
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuantity([FromBody] UpdateCartItemRequest request, CancellationToken cancellationToken)
        {
            var userId = userManager.GetUserId(User);
            await cartService.UpdateCartItemQuantityAsync(request, userId, cancellationToken);

            var updatedCart = await cartService.GetCartByUserIdAsync(
                userManager.GetUserId(User)!,
                webHostEnvironment.WebRootPath,
                cancellationToken);

            return Json(new
            {
                success = true,
                totalItems = updatedCart.TotalItems,
                totalPrice = updatedCart.TotalPrice,
                itemPrice = updatedCart.Items.FirstOrDefault(x => x.Id == request.ProductId)?.Price * request.Quantity
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetCartCount(CancellationToken cancellationToken)
        {
            try
            {
                var userId = userManager.GetUserId(User);
                var cart = await cartService.GetCartByUserIdAsync(
                    userId!,
                    webHostEnvironment.WebRootPath,
                    cancellationToken);

                return Json(new { success = true, count = cart.TotalItems });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
} 