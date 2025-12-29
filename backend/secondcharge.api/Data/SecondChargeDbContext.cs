using Microsoft.EntityFrameworkCore;
using secondcharge.api.Models.Domain;

namespace secondcharge.api.Data
{
    public class SecondChargeDbContext: DbContext
    {
        public SecondChargeDbContext(DbContextOptions<SecondChargeDbContext> dbContextOptions) : base(dbContextOptions) 
        { 
        
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<VehicleListing> VehicleListings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for Locations
            // 

            var locations = new List<Location>
            {
                new Location { Id = Guid.Parse("43b494e9-9d33-421f-85a3-62cc58dd7e99"), Country = "USA", State = "NJ", zipCode = "94820"},
                new Location { Id = Guid.Parse("9954836d-5a9f-4d3f-b156-12f32ff0306a"), Country = "IR", State = "DUB", zipCode = "43 9VA"},
                new Location { Id = Guid.Parse("6a8ad14d-8482-4ff4-8f82-28abd6ddc21a"), Country = "NR", State = "OSL", zipCode = "333 5BA"},
            };

            // Seed difficulties to the database
            modelBuilder.Entity<Location>().HasData(locations);
        }
    }
}
