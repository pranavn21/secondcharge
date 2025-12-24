using Microsoft.EntityFrameworkCore;
using secondcharge.api.Data;
using secondcharge.api.Models.Domain;

namespace secondcharge.api.Repositories
{
    public class SQLVehicleListingRepository : IVehicleListingRepository
    {
        private readonly SecondChargeDbContext dbContext;

        public SQLVehicleListingRepository(SecondChargeDbContext secondChargeDbContext)
        {
            this.dbContext = secondChargeDbContext;
        }

        public async Task<VehicleListing> CreateAsync(VehicleListing vehicleListing)
        {
            await dbContext.VehicleListings.AddAsync(vehicleListing);
            await dbContext.SaveChangesAsync();
            return vehicleListing;
        }

        public async Task<VehicleListing?> DeleteAsync(Guid id)
        {
            var existingVehicleListing = await dbContext.VehicleListings.FirstOrDefaultAsync(x => x.Id == id);
            if (existingVehicleListing == null)
            {
                return null;
            }

            dbContext.VehicleListings.Remove(existingVehicleListing);
            await dbContext.SaveChangesAsync();
            return existingVehicleListing;
        }

        public async Task<List<VehicleListing>> GetAllVehicleListingsAsync()
        {
            return await dbContext.VehicleListings.ToListAsync();
        }

        public async Task<VehicleListing?> GetVehicleListingByIdAsync(Guid id)
        {
            return await dbContext.VehicleListings.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<VehicleListing?> UpdateAsync(Guid id, VehicleListing vehicleListing)
        {
            var existingVehicleListing = await dbContext.VehicleListings.FirstOrDefaultAsync(x => x.Id == id);
            if (existingVehicleListing == null)
            {
                return null;
            }
            existingVehicleListing.CarId = vehicleListing.CarId;
            existingVehicleListing.Mileage = vehicleListing.Mileage;
            existingVehicleListing.Color = vehicleListing.Color;
            existingVehicleListing.listingLocationId = vehicleListing.listingLocationId;
            existingVehicleListing.Price = vehicleListing.Price;

            await dbContext.SaveChangesAsync();
            return existingVehicleListing;
        }
    }
}
