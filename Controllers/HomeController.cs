using AdvFullstack_Labb2.Helpers;
using AdvFullstack_Labb2.Models;
using AdvFullstack_Labb2.Services.IServices;
using AdvFullstack_Labb2.ViewModels.DisplayVMs;
using AdvFullstack_Labb2.ViewModels.OtherVMs;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AdvFullstack_Labb2.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly IApiClient _client;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IApiClient client, ILogger<HomeController> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var menuItems = await _client.GetAllAsync<MenuItem>(ApiRoutes.MenuItem.Base);
                // Get menuItems
                var topThreeMenuItems = menuItems?
                    .Where(mi => mi.IsPopular == true)
                    .Take(3)
                    .Select(mi => new MenuItemPublicVM
                    {
                        Name = mi.Name,
                        Price = mi.Price,
                        ImageUrl = mi.ImageUrl
                    }).ToList() ?? new List<MenuItemPublicVM>();
                // Select 3 menuItems that are also popular

                return View(topThreeMenuItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load menuitems for home/index");
                TempData["Error"] = "The menu is temporarily unavailable due to server issues";
                return View(new List<MenuItemPublicVM>());
            }
        }

        [Route("/Home/StatusCode/{statusCode}")]
        public IActionResult StatusCode(int statusCode)
        {
            var vm = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                StatusCode = statusCode
            };

            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "The page you're looking for doesn't exist.";
                    ViewBag.ErrorTitle = "Page Not Found";
                    ViewBag.ErrorSubtitle = "Sorry, but the page you are looking for has not been found.";
                    return View("NotFound", vm);

                case 403:
                    ViewBag.ErrorMessage = "You don't have permission to access this resource.";
                    ViewBag.ErrorTitle = "Access Forbidden";
                    ViewBag.ErrorSubtitle = "You are not authorized to view this page.";
                    return View("Forbidden", vm);

                case 500:
                    ViewBag.ErrorMessage = "An internal server error occurred.";
                    ViewBag.ErrorTitle = "Server Error";
                    ViewBag.ErrorSubtitle = "Something went wrong on our end.";
                    return View("ServerError", vm);

                default:
                    ViewBag.ErrorMessage = "An error occurred while processing your request.";
                    ViewBag.ErrorTitle = $"Error {statusCode}";
                    ViewBag.ErrorSubtitle = "Please try again later.";
                    return View("Error", vm);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }

        public IActionResult ErrorApiDown()
        {
            return View();
        }

        [Route("Home/HandleException")]
        public IActionResult HandleException()
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionHandlerPathFeature?.Error is InvalidOperationException &&
                exceptionHandlerPathFeature.Error.Message.Contains("The view") &&
                exceptionHandlerPathFeature.Error.Message.Contains("was not found"))
            {
                ViewBag.ErrorTitle = "Missing View";
                ViewBag.ErrorMessage = "The page you tried to access is not available or the view is missing.";
                ViewBag.ErrorSubtitle = "Please contact the administrator or try another link.";

                var vm = new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    StatusCode = 500
                };

                return View("MissingView", vm);
            }

            // For all other exceptions
            ViewBag.ErrorTitle = "Something went wrong.";
            ViewBag.ErrorMessage = "An unexpected error occurred.";
            ViewBag.ErrorSubtitle = "Please try again later.";

            return View("Error", new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                StatusCode = 500
            });
        }
    }
}
