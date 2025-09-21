using AdvFullstack_Labb2.Helpers;
using AdvFullstack_Labb2.Models;
using AdvFullstack_Labb2.Services.IServices;
using AdvFullstack_Labb2.ViewModels.DisplayVMs;
using AdvFullstack_Labb2.ViewModels.EditCreateVMs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AdvFullstack_Labb2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenuItemsController : BaseAdminController
    {
        public MenuItemsController(IApiClient client)
            : base(client) { }

        public async Task<IActionResult> Index()
        {
            ViewData["NavbarTitle"] = "Menu Item Administration";
            var menuItems = await _client.GetAllAsync<MenuItemAdminVM>(ApiRoutes.MenuItem.Base);

            return View(menuItems);
        }

        public IActionResult Create()
        {
            ViewData["NavbarTitle"] = "Create Menu Item";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MenuItemAdminVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var imageUrl = string.IsNullOrEmpty(vm.ImageUrl) ? "None" : vm.ImageUrl;

            var menuItem = new MenuItem
            {
                Name = vm.Name,
                Price = vm.Price,
                IsPopular = vm.IsPopular,
                ImageUrl = imageUrl
            };

            var response = await _client.PostAsync<MenuItem, CreatedResponse>(ApiRoutes.MenuItem.Base, menuItem);

            if (response == null)
            {
                ModelState.AddModelError("", "Failed to create a menuItem");

                return View(vm);
            }

            TempData["Success"] = $"Created new Menu item with id {response.Id}";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewData["NavbarTitle"] = "Edit Menu Item";
            var menuItem = await _client.GetByIdAsync<MenuItem>(ApiRoutes.MenuItem.GetById(id));

            if (menuItem == null)
            {
                TempData["Error"] = $"Failed to fetch Menu item with id: {id}";
                return RedirectToAction("Index");
            }

            var vm = new MenuItemEditVM
            {
                Id = menuItem.Id,
                Name = menuItem.Name,
                Price = menuItem.Price,
                IsPopular = menuItem.IsPopular,
                ImageUrl = menuItem.ImageUrl
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MenuItemEditVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm); 
            }

            var imageUrl = string.IsNullOrEmpty(vm.ImageUrl) ? "None" : vm.ImageUrl;

            var menuItem = new MenuItem
            {
                Id = vm.Id,
                Name = vm.Name,
                Price = vm.Price,
                IsPopular = vm.IsPopular,
                ImageUrl = imageUrl
            };

            var response = await _client.PutAsync<MenuItem, object>(ApiRoutes.MenuItem.GetById(menuItem.Id), menuItem);

            TempData["Success"] = $"Updated Menu item with id: {vm.Id}";
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _client.DeleteAsync(ApiRoutes.MenuItem.GetById(id));

            if (!success)
            {
                TempData["Error"] = $"Failed to delete Menu item with id {id}";
                return RedirectToAction("Index");
            }

            TempData["Success"] = $"Deleted Menu item with id: {id}";
            return RedirectToAction("Index");
        }
    }
}
