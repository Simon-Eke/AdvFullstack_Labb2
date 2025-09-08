using AdvFullstack_Labb2.Helpers;
using AdvFullstack_Labb2.Models;
using AdvFullstack_Labb2.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AdvFullstack_Labb2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenuItemsController : BaseController
    {
        public MenuItemsController(IApiClient client)
            : base(client) { }

        public async Task<IActionResult> Index()
        {
            ViewData["NavbarTitle"] = "Menu Item Administration";
            var menuItems = await _client.GetAllAsync<MenuItem>(ApiRoutes.MenuItem.Base);

            return View(menuItems);
        }

        public IActionResult Create()
        {
            ViewData["NavbarTitle"] = "Create Menu Item";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MenuItem menuItem)
        {
            if (!ModelState.IsValid)
            {
                return View(menuItem);
            }

            var response = await _client.PostAsync<MenuItem, CreatedResponse>(ApiRoutes.MenuItem.Base, menuItem);

            if (response != null)
            {
                // TempData["Success"] = $"Created new Menu item with id {response.Id}";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Failed to create a menuItem");

            return View(menuItem);
        }


        public async Task<IActionResult> Edit(int id)
        {
            ViewData["NavbarTitle"] = "Edit Menu Item";
            var menuItem = await _client.GetByIdAsync<MenuItem>(ApiRoutes.MenuItem.GetById(id));

            return View(menuItem);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MenuItem menuItem)
        {
            if (!ModelState.IsValid)
            {
                return View(menuItem); 
            }


            var response = await _client.PutAsync<MenuItem, object>(ApiRoutes.MenuItem.Base, menuItem);

            if (response == null)
            {
                ModelState.AddModelError("", "Could not update menuItem. ");
                return View(menuItem);
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _client.DeleteAsync(ApiRoutes.MenuItem.GetById(id));

            if (!success)
            {
                // TempData["Error"] = $"Failed to delete Menu item with id {id}";
                return RedirectToAction("Index");
            }
            // TempData["Success"] = $"Deleted Menu item with id {id}";
            return RedirectToAction("Index");
        }
    }
}
