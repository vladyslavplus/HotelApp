using HotelApp.Data;
using HotelApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HotelApp.Controllers
{
    public class BookingController : Controller
    {
        private readonly HotelDbContext _context;
        private readonly UserManager<Users> _userManager;

        public BookingController(HotelDbContext context, UserManager<Users> userManager)
        {
            _context = context;
            _userManager = userManager;
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Console.WriteLine($"UserId: {userId}");
            if (string.IsNullOrEmpty(userId))
            {
                ModelState.AddModelError("UserId", "User not logged in or UserId not found.");
                return RedirectToAction("Login", "Account");
            }

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
                Cuisine = CuisineType.Loval,
                UserId = userId
            };

            Console.WriteLine($"Booking created with Hotel: {selectedHotel.Name}");

            return View(booking);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Details(Booking booking)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                ModelState.AddModelError("UserId", "User is not authenticated.");
                return View(booking);
            }

            booking.UserId = userId;

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

            return View(booking);
        }




        [Authorize]
        [Route("Bookings/Info")]
        public async Task<IActionResult> Info()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var bookings = await _context.Bookings
                .Where(b => b.UserId == userId)
                .Include(b => b.Hotel)
                .ToListAsync();

            return View(bookings);
        }
        [Authorize]
        [HttpGet]   
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(Booking booking)
        {
            if (ModelState.IsValid)
            {
                _context.Update(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction("Info");
            }
            return View(booking);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeletePOST(int? id)
        {
            if (ModelState.IsValid)
            {
                var booking = await _context.Bookings.FindAsync(id);
                if (booking == null)
                {
                    return NotFound();
                }
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();

                return RedirectToAction("Info");
            }
            return View();
        }
    }
}