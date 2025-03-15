using Microsoft.AspNetCore.Mvc;
using WebShop.Core.Models.Authorization;
using Microsoft.AspNetCore.Identity;
using WebShop.Sql.Models;

namespace WebShop.Presentation.Controllers
{
    public class AuthorizationController(
        SignInManager<User> signInManager,
        UserManager<User> userManager) : Controller
    {
        [HttpGet]
        public IActionResult SignupPage()
        {
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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser(UserLoginCredentialsViewModel credentials)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(LoginPage), credentials);
            }

            await signInManager.PasswordSignInAsync(
                credentials.UserName,
                credentials.Password,
                isPersistent: false,
                lockoutOnFailure: false);
            return RedirectToAction(nameof(ProductsController.GridView), "Products");
        }
    }
}
