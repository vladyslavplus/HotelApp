using HotelApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelApp.Data
{
    public class HotelDbContext : IdentityDbContext<Users>
    {
        public HotelDbContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
