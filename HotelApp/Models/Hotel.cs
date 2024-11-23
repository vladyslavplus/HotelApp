using System.ComponentModel.DataAnnotations;

namespace HotelApp.Models
{
    public class Hotel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [MaxLength(200)]
        public string Address { get; set; } = null!;

        [Range(1, 5)]
        public int Stars { get; set; }

        [MaxLength(500)]
        public string Description { get; set; } = null!;

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
