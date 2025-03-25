using Microsoft.AspNetCore.Mvc;
using WebShop.Core.Models.Authorization;
using Microsoft.AspNetCore.Identity;
using WebShop.Sql.Models;
using WebShop.Sql;

namespace WebShop.Presentation.Controllers
{
    public class AuthorizationController(
        SignInManager<User> signInManager,
        UserManager<User> userManager,
        WebShopContext dbContext) : Controller
    {
        [HttpGet]
        public IActionResult SignupPage()
        {
            ViewData["ShowNavbar"] = true;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignupUser(UserSignupCredentialsViewModel credentials, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(SignupPage), credentials);
            }

            var user = new User
            {
                UserName = credentials.UserName,
                Email = credentials.Email,
                PhoneNumber = credentials.PhoneNumber,
            };

            var result = await userManager.CreateAsync(user, credentials.Password);

            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction(nameof(ProductsController.GridView), "Products");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(nameof(SignupPage), credentials);
        }

        [HttpGet]
        public IActionResult LoginPage()
        {
            ViewData["ShowNavbar"] = true;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser(UserLoginCredentialsViewModel credentials)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(LoginPage), credentials);
            }

            var user = await signInManager.PasswordSignInAsync(
                credentials.UserName,
                credentials.Password,
                isPersistent: false,
                lockoutOnFailure: false);

            if (!user.Succeeded)
            {
                ModelState.AddModelError("Username", "Username or password is wrong.");
                return View(nameof(LoginPage), credentials);
            }

                return RedirectToAction(nameof(ProductsController.GridView), "Products");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(ProductsController.GridView), "Products");
        }
    }
}
