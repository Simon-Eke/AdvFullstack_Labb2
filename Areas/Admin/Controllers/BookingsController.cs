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
            // Get all bookings
            // Get all customers
            // Get all tables

            // create a table with booking date, customer amount, table number, seatings, customer name, customer phone
            return View();
        }

        // Create
        // Create
        // Edit 
        // Edit
        // Delete
    }
}
