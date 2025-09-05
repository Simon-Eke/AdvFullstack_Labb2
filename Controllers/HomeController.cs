using System.Diagnostics;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AdvFullstack_Labb2.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AdvFullstack_Labb2.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly HttpClient _client;

        public HomeController(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient("MyCafeApi");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync("tables");

            var json = await response.Content.ReadAsStringAsync();

            var tableList = JsonConvert.DeserializeObject<List<Table>>(json);
            return View(tableList);
        }
    }
}
