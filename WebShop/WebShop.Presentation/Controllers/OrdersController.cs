using Microsoft.AspNetCore.Mvc;
using WebShop.Core.Models.Order;
using Microsoft.AspNetCore.Authorization;
using WebShop.Core.Interfaces;

namespace WebShop.Presentation.Controllers
{
    [Authorize]
    public class OrdersController(ICartService cartService, IOrderService orderService, IWebHostEnvironment webHostEnvironment) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> CreateOrderPage(CancellationToken cancellationToken)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("LoginPage", "Authorization");

            var cart = await cartService.GetCartByUserIdAsync(userId, webHostEnvironment.WebRootPath, cancellationToken);
            if (!cart.Items.Any())
                return RedirectToAction("CartPage", "Cart");

            ViewData["ShowNavbar"] = true;

            var viewModel = new CheckoutViewModel
            {
                TotalPrice = cart.TotalPrice
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitOrder(CheckoutViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return View("CreateOrderPage", model);

            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("LoginPage", "Authorization");

            var publicOrderNumber = await orderService.CreateOrderAsync(userId, model, cancellationToken);

            return RedirectToAction("OrderSuccessfullyCreatedPage", new { publicOrderNumber });
        }

        [HttpGet]
        public IActionResult OrderSuccessfullyCreatedPage([FromQuery] long publicOrderNumber)
        {
            return View(publicOrderNumber);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderDetails(long orderNumber, CancellationToken cancellationToken)
        {
            var viewModel = await orderService.GetOrderDetailsAsync(orderNumber, webHostEnvironment.WebRootPath, cancellationToken);

            return PartialView("_OrderDetailsPartial", viewModel);
        }
    }
} 