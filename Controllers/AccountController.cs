using AdvFullstack_Labb2.Services.IServices;
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
        public async Task<IActionResult> Login([FromForm] string username, [FromForm] string password)
        {
            var jwt = await _client.LoginAsync(username, password);

            if (jwt == null)
            {
                ModelState.AddModelError("", "Invalid Login");
                return View();
            }

            Response.Cookies.Append("JWToken", jwt, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddHours(1)
            });

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("JWToken");
            return RedirectToAction("Index", "Home");
        }

    }
}
