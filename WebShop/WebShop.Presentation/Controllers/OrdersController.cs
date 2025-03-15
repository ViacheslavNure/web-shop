using Microsoft.AspNetCore.Mvc;

namespace WebShop.Presentation.Controllers
{
    public class OrdersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
