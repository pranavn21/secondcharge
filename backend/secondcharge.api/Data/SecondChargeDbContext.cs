using Microsoft.EntityFrameworkCore;
using secondcharge.api.Models.Domain;

namespace secondcharge.api.Data
{
    public class SecondChargeDbContext: DbContext
    {
        public SecondChargeDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) 
        { 
        
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<VehicleListing> VehicleListings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Location> Locations { get; set; }
    }
}
