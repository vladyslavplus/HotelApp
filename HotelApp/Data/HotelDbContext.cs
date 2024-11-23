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

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Hotel> Hotels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Hotel>().HasData(
                new Hotel
                {
                    Id = 1,
                    Name = "Hotel Paradise",
                    Address = "123 Paradise Rd, Tropical City",
                    Stars = 5,
                    Description = "A luxury hotel offering the ultimate comfort and breathtaking views."
                },
                new Hotel
                {
                    Id = 2,
                    Name = "Ocean View Resort",
                    Address = "456 Ocean Ave, Coastal Town",
                    Stars = 4,
                    Description = "Enjoy your stay with ocean views and a relaxing atmosphere."
                },
                new Hotel
                {
                    Id = 3,
                    Name = "Mountain Retreat",
                    Address = "789 Summit St, Alpine Village",
                    Stars = 3,
                    Description = "A cozy retreat surrounded by stunning mountain scenery."
                },
                new Hotel
                {
                    Id = 4,
                    Name = "City Lights Hotel",
                    Address = "321 Downtown Blvd, Metropolis",
                    Stars = 4,
                    Description = "Stay in the heart of the city and enjoy modern amenities."
                },
                new Hotel
                {
                    Id = 5,
                    Name = "Desert Oasis Inn",
                    Address = "654 Sand Dunes Rd, Desert Haven",
                    Stars = 3,
                    Description = "A peaceful inn surrounded by serene desert landscapes."
                }
            );
        }
    } 
}
