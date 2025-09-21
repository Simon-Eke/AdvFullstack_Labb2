using AdvFullstack_Labb2.Models;
using AdvFullstack_Labb2.Services.IServices;
using AdvFullstack_Labb2.ViewModels.EditCreateVMs;
using Microsoft.AspNetCore.Mvc;

namespace AdvFullstack_Labb2.Controllers
{
    public class AccountController : Controller
    {
        private readonly IApiAuthClient _client;

        public AccountController(IApiAuthClient client)
        {
            _client = client;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginFormVM vm)
        {

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var loginRequest = new LoginRequest
            {
                Username = vm.Username,
                Password = vm.Password
            };

            var jwt = await _client.LoginAsync(loginRequest);

            if (jwt == null)
            {
                ModelState.AddModelError("", "Logga in misslyckades! Ditt lösenord och/eller användarnamn är felaktigt.");
                return View(vm);
            }

            Response.Cookies.Append("JWToken", jwt, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddHours(1)
            });

            TempData["Success"] = "Inloggning lyckades!";

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("JWToken");
            TempData["Success"] = "Utloggning lyckades!";
            return RedirectToAction("Index", "Home");
        }

    }
}
