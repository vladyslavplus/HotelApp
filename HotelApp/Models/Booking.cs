using System.ComponentModel.DataAnnotations;

namespace HotelApp.Models
{
    public enum SuiteType
    {
        Standard,
        Deluxe,
        Executive,
        Presidential
    }

    public enum CuisineType
    {
        Loval,
        Multi
    }
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }

        [Required]
        public string UserId { get; set; }
        public Users? User { get; set; }

        [Range(1, 6, ErrorMessage = "Number of adults must be between 1 and 6.")]
        public int NoOfAdults { get; set; }

        [Range(0, 6, ErrorMessage = "Number of children must be between 0 and 6.")]
        public int NoOfChildren { get; set; }

        [Required(ErrorMessage = "Suite is required.")]
        public SuiteType Suite { get; set; }

        [Required(ErrorMessage = "Check-in date is required.")]
        [DataType(DataType.Date)]
        public DateTime CheckIn { get; set; }

        [Required(ErrorMessage = "Check-out date is required.")]
        [DataType(DataType.Date)]
        [CheckOutDate]
        public DateTime CheckOut { get; set; }

        [Required(ErrorMessage = "Cuisine preference is required.")]
        public CuisineType Cuisine { get; set; }
    }
    public class CheckOutDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            var booking = (Booking)validationContext.ObjectInstance;
            if (booking.CheckOut <= booking.CheckIn)
            {
                return new ValidationResult("Check-out date must be later than check-in date.");
            }
            return ValidationResult.Success;
        }
    }
}

    
