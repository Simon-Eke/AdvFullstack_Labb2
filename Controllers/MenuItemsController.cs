using AdvFullstack_Labb2.Services.IServices;
using AdvFullstack_Labb2.Helpers;
using AdvFullstack_Labb2.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdvFullstack_Labb2.Controllers
{
    public class MenuItemsController : BaseController
    {
        public MenuItemsController(IApiClient _client, IHttpContextAccessor accessor)
            : base(_client, accessor) { }

        public async Task<IActionResult> Index()
        {
            var menuItems = await _client.GetAllAsync<MenuItem>(ApiRoutes.MenuItem.Base);

            return View(menuItems);
        }
    }
}
