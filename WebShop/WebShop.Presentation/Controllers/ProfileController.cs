using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShop.Core.Interfaces;

namespace WebShop.Presentation.Controllers
{
    [Authorize]
    public class ProfileController(IUserProfileService userProfileService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> UserProfile()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("LoginPage", "Authorization");

           var userProfile = await userProfileService.GetUserProfileBuUserId(userId, CancellationToken.None);

            ViewData["ShowNavbar"] = true;
            return View(userProfile);
        }
    }
} 