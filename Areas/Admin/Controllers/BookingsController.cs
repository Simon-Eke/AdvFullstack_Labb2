using AdvFullstack_Labb2.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace AdvFullstack_Labb2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookingsController : BaseController
    {
        public BookingsController(IApiClient client)
            : base(client) { }
        public IActionResult Index()
        {
            return View();
        }
    }
}
