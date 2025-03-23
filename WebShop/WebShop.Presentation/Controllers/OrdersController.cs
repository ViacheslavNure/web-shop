using Microsoft.AspNetCore.Mvc;
using WebShop.Core.Models.Order;
using Microsoft.AspNetCore.Authorization;
using WebShop.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using WebShop.Core.Models.Cart;

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

            var publicOrerNumber = await orderService.CreateOrderAsync(userId, webHostEnvironment.WebRootPath, model, cancellationToken);

            return RedirectToAction("OrderSuccessfullyCreatedPage", new { publicOrerNumber });
        }

        [HttpGet]
        public IActionResult OrderSuccessfullyCreatedPage([FromQuery] long publicOrderNumber)
        {
            return View(publicOrderNumber);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderDetails(ulong orderNumber)
        {
            // Моковые данные для демонстрации
            var viewModel = new OrderViewModel
            {
                Id = Guid.NewGuid(),
                PublicOrderNumber = orderNumber,
                DeliveryAddress = "вул. Шевченка, 15, кв. 45",
                City = "Київ",
                PostalCode = "01001",
                DeliveryMethod = DeliveryMethod.NovaPoshta,
                CardNumber = "4111111111111111",
                CardExpiry = "12/25",
                CardCvv = "123",
                OrderComments = "Будь ласка, дзвоніть перед доставкою",
                TotalPrice = 2499.99m,
                Items = new List<CartItemViewModel>
                {
                    new CartItemViewModel
                    {
                        Id = Guid.NewGuid(),
                        Brand = "Apple",
                        Model = "iPhone 15 Pro",
                        Price = 1499.99m,
                        ImagePath = "/images/products/iphone15pro.jpg",
                        Quantity = 1
                    },
                    new CartItemViewModel
                    {
                        Id = Guid.NewGuid(),
                        Brand = "Apple",
                        Model = "AirPods Pro",
                        Price = 500.00m,
                        ImagePath = "/images/products/airpodspro.jpg",
                        Quantity = 2
                    }
                }
            };

            return PartialView("_OrderDetailsPartial", viewModel);
        }
    }
} 