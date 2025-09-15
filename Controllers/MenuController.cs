using AdvFullstack_Labb2.Helpers;
using AdvFullstack_Labb2.Models;
using AdvFullstack_Labb2.Services.IServices;
using AdvFullstack_Labb2.ViewModels.DisplayVMs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AdvFullstack_Labb2.Controllers
{
    public class MenuController : Controller
    {
        private readonly IApiClient _client;

        public MenuController(IApiClient client)
        {
            _client = client;
        }
        public async Task<IActionResult> Index()
        {
            // Get all menuItems
            var model = await _client.GetAllAsync<MenuItem>(ApiRoutes.MenuItem.Base);
            var menu = model
                .OrderBy(mi => mi.IsPopular == true)
                .Select(mi => new MenuItemPublicVM
                    {
                        Name = mi.Name,
                        Price = mi.Price,
                        ImageUrl = mi.ImageUrl
                    }).ToList();

            return View(menu);
        }
    }
}
