using Microsoft.AspNetCore.Mvc;

namespace AdvFullstack_Labb2.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
