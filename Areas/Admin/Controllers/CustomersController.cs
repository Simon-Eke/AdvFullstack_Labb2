using AdvFullstack_Labb2.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace AdvFullstack_Labb2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomersController : BaseAdminController
    {
        public CustomersController(IApiClient client)
            : base(client) { }
        public IActionResult Index()
        {
            // Lista alla kunder
            return View();
        }

        // Delete
    }
}
