using AdvFullstack_Labb2.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace AdvFullstack_Labb2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TablesController : BaseAdminController
    {
        public TablesController(IApiClient client)
            : base(client) { }
        public IActionResult Index()
        {
            // Get all tables
            return View();
        }

        // Create
        // Create
        // Edit 
        // Edit
        // Delete
    }
}
