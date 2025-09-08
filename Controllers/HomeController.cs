using System.Diagnostics;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AdvFullstack_Labb2.Models;
using AdvFullstack_Labb2.Helpers;
using AdvFullstack_Labb2.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
            var tableList = await _client.GetAllAsync<Table>(ApiRoutes.Table.Base);

            return View(tableList);
        }
    }
}
