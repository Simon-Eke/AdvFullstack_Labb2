using System.Diagnostics;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AdvFullstack_Labb2.Models;
using AdvFullstack_Labb2.Helpers;
using AdvFullstack_Labb2.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using AdvFullstack_Labb2.ViewModels.DisplayVMs;

namespace AdvFullstack_Labb2.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly IApiClient _client;

        public HomeController(IApiClient client)
        {
            _client = client;
        }

        public async Task<IActionResult> Index()
        {
            var menuItems = await _client.GetAllAsync<MenuItem>(ApiRoutes.MenuItem.Base);
            // Get menuItems
            var topThreeMenuItems = menuItems
                .Where(mi => mi.IsPopular == true)
                .Take(3)
                .Select(mi => new MenuItemPublicVM
                {
                    Name = mi.Name,
                    Price = mi.Price,
                    ImageUrl = mi.ImageUrl
                }).ToList();
            // Select 3 menuItems that are also popular

            return View(topThreeMenuItems);
        }
    }
}
