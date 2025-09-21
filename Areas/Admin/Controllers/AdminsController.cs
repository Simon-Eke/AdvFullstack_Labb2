using AdvFullstack_Labb2.Helpers;
using AdvFullstack_Labb2.Models;
using AdvFullstack_Labb2.Services.IServices;
using AdvFullstack_Labb2.ViewModels.DisplayVMs;
using AdvFullstack_Labb2.ViewModels.EditCreateVMs;
using Microsoft.AspNetCore.Mvc;

namespace AdvFullstack_Labb2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminsController : BaseAdminController
    {
        private readonly IApiAuthClient _authClient;
        public AdminsController(IApiClient client, IApiAuthClient authClient)
            : base(client) 
        {
            _authClient = authClient;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["NavbarTitle"] = "Admin Administration";
            var admins = await _client.GetAllAsync<AdminVM>(ApiRoutes.Admin.Base);
            // Create table of admins
            return View(admins);
        }
        
        // Create
        public IActionResult Create()
        {
            ViewData["NavbarTitle"] = "Create Admin";
            return View();
        }

        // Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var admin = new AdminModel
            {
                Username = vm.Username,
                Password = vm.Password
            };

            var response = await _client.PostAsync<AdminModel, CreatedResponse>(ApiRoutes.Admin.Base, admin);

            if (response == null)
            {
                ModelState.AddModelError("", "Failed to create an Admin");

                return View(vm);
            }

            TempData["Success"] = $"Created new Admin with id {response.Id}";
            return RedirectToAction("Index");
        }

        // Edit 

        public async Task<IActionResult> Edit(int id)
        {
            ViewData["NavbarTitle"] = "Edit Admin";
            var admin = await _client.GetByIdAsync<AdminModel>(ApiRoutes.Admin.GetById(id));

            if (admin == null)
            {
                TempData["Error"] = $"Failed to fetch Admin with id: {id}";
                return RedirectToAction("Index");
            }

            var vm = new AdminEditVM
            {
                Id = id,
                Username = admin.Username,
                Step = EditStep.PasswordConfirm
            };

            return View(vm);
        }

        // ConfirmPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmPassword(AdminEditVM vm)
        {
            // Quick fix, the admineditvm should be made into 2 vm:s but oh well.

            ModelState.Remove("NewPassword");
            ModelState.Remove("ConfirmNewPassword");
            // kolla modelstate
            if (!ModelState.IsValid)
            {
                vm.Step = EditStep.PasswordConfirm;
                return View("Edit", vm);
            }

            // skicka till authlogin

            var loginRequest = new LoginRequest
            {
                Username = vm.Username,
                Password = vm.CurrentPassword
            };

            var loginResponse = await _authClient.LoginAsync(loginRequest);



            if (loginResponse == null)
            {
                ModelState.AddModelError("CurrentPassword", "Ditt lösenord är felaktigt.");
                vm.Step = EditStep.PasswordConfirm;
                return View("Edit", vm);
            }

            // om success
            //   ändra step till editform
            vm.Step = EditStep.EditForm;
            //   nollställ lösenord
            vm.CurrentPassword = null;
            //   skicka till edit
            return View("Edit", vm);
        }

        // Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(AdminEditVM vm)
        {
            // Quick fix, the adminEditVm should be made into 2 vm:s but oh well.
            ModelState.Remove("CurrentPassword");
            // Kolla modelstate
            if (!ModelState.IsValid)
            {
                vm.Step = EditStep.EditForm;
                return View("Edit", vm);
            }

            // New password can be empty/null due to custom attribute and 
            // Service in api that ensures correctness for put requests
            var adminToUpdate = new AdminModel
            {
                Id = vm.Id,
                Username = vm.Username,
                Password = string.IsNullOrWhiteSpace(vm.NewPassword) ? null : vm.NewPassword
            };

            // If failed then the global filter will take care of it.
            // Stored in response for future use if necessary.
            var response = await _client.PutAsync<AdminModel, object>(ApiRoutes.Admin.GetById(vm.Id), adminToUpdate);

            TempData["Success"] = $"Updated Admin with id: {vm.Id}";
            return RedirectToAction("Index");
            /* 
            try
            {
                Console.WriteLine($"Update called - Id: {vm.Id}, Username: {vm.Username}, NewPassword: {(string.IsNullOrEmpty(vm.NewPassword) ? "empty" : "has value")}");

                // Remove validation for fields not relevant to this step
                ModelState.Remove("NewPassword");
                ModelState.Remove("ConfirmNewPassword");
                ModelState.Remove("CurrentPassword"); // ADD THIS LINE

                if (!ModelState.IsValid)
                {
                    Console.WriteLine("ModelState invalid");
                    foreach (var error in ModelState)
                    {
                        Console.WriteLine($"Key: {error.Key}, Errors: {string.Join(", ", error.Value.Errors.Select(e => e.ErrorMessage))}");
                    }
                    vm.Step = EditStep.EditForm;
                    return View("Edit", vm);
                }

                Console.WriteLine("Creating admin model to update");
                var adminToUpdate = new AdminModel
                {
                    Id = vm.Id,
                    Username = vm.Username,
                    Password = string.IsNullOrWhiteSpace( vm.NewPassword) ? null : vm.NewPassword
                };

                Console.WriteLine($"Calling API with route: {ApiRoutes.Admin.GetById(vm.Id)}");
                var response = await _client.PutAsync<AdminModel, object>(ApiRoutes.Admin.GetById(vm.Id), adminToUpdate);

                Console.WriteLine("API call successful");
                TempData["Success"] = $"Updated Admin with id: {vm.Id}";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in Update: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
            
            */
        }


        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _client.DeleteAsync(ApiRoutes.Admin.GetById(id));

            if (!success)
            {
                TempData["Error"] = $"Failed to delete Admin with id {id}";
                return RedirectToAction("Index");
            }

            TempData["Success"] = $"Deleted Admin with id: {id}";
            return RedirectToAction("Index");
        }
    }
}
