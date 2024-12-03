using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HotelApp.Models
{
    public class Users : IdentityUser
    {
        public string FullName { get; set; } = null!;
        [Phone]
        public string? Phone { get; set; }
        public string? AvatarPath { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
