using Microsoft.AspNetCore.Mvc;

namespace AdvFullstack_Labb2.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            // Get all menuItems
            return View();
        }
    }
}
