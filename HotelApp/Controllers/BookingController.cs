using HotelApp.Data;
using HotelApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelApp.Controllers
{
    public class BookingController : Controller
    {
        private readonly HotelDbContext _context;
        public BookingController(HotelDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {
            var hotels = await _context.Hotels.ToListAsync();
            return View(hotels);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Details(int hotelId)
        {
            var selectedHotel = await _context.Hotels
                                   .FirstOrDefaultAsync(h => h.Id == hotelId);

            if (selectedHotel == null)
            {
                return NotFound();
            }

            var booking = new Booking
            {
                HotelId = selectedHotel.Id,
                Hotel = selectedHotel,
                NoOfAdults = 1,  
                NoOfChildren = 0,
                Suite = SuiteType.Standard,
                CheckIn = DateTime.Now,
                CheckOut = DateTime.Now.AddDays(1),
                Cuisine = CuisineType.Loval
            };

            Console.WriteLine($"Booking created with Hotel: {selectedHotel.Name}");

            return View(booking);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Details(Booking booking)
        {
            Console.WriteLine($"HotelId: {booking.HotelId}");

            if (ModelState.IsValid)
            {
                var selectedHotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == booking.HotelId);

                if (selectedHotel != null)
                {
                    booking.Hotel = selectedHotel;
                    _context.Bookings.Add(booking);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Info");
                }
                else
                {
                    ModelState.AddModelError("Hotel", "The selected hotel does not exist.");
                }
            }

            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine($"Validation error: {error.ErrorMessage}");
            }

            return View(booking);
        }
        [Authorize]
        [Route("Bookings/Info")]
        public async Task<IActionResult> Info()
        {
            var bookings = await _context.Bookings
                .Include(b => b.Hotel) 
                .ToListAsync();

            return View(bookings); 
        }



    }
}
