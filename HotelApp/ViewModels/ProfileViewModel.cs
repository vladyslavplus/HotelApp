using System.ComponentModel.DataAnnotations;

namespace HotelApp.ViewModels
{
    public class ProfileViewModel
    {
        public string FullName { get; set; } = null!;
        [Phone]
        public string? Phone { get; set; }
    }
}
