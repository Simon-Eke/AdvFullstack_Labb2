using AdvFullstack_Labb2.Helpers;
using AdvFullstack_Labb2.Models;
using AdvFullstack_Labb2.Services.IServices;
using AdvFullstack_Labb2.ViewModels.DisplayVMs;
using AdvFullstack_Labb2.ViewModels.EditCreateVMs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AdvFullstack_Labb2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookingsController : BaseAdminController
    {
        public BookingsController(IApiClient client)
            : base(client) { }
        public async Task<IActionResult> Index()
        {
            // Get all bookingswithdetails
            var bookings = await _client.GetAllAsync<BookingWithDetails>(ApiRoutes.Booking.Base + "/with-details");

            var bookingsDto = bookings.Select(b => new BookingVM
            {
                Id = b.Id,
                StartTime = b.StartTime,
                Name = b.Customer.Name,
                PhoneNumber = b.Customer.PhoneNumber,
                Seatings = b.Table.Seatings,
                CustomerAmount = b.CustomerAmount,
                TableNumber = b.Table.TableNumber
            }).ToList();


            // create a table with booking date, customer amount, table number, seatings, customer name, customer phone
            return View(bookingsDto);
        }

        // Create
        // Create
        // Edit 
        // Edit
        // Delete
    }
}
