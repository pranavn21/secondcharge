using Microsoft.EntityFrameworkCore;
using secondcharge.api.Data;
using secondcharge.api.Models.Domain;
using secondcharge.api.Repositories.Interfaces;

namespace secondcharge.api.Repositories.SQL
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

        public async Task<List<VehicleListing>> GetAllVehicleListingsAsync(string? filterOn = null, string? filterQuery = null, 
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            var vehicleListings = dbContext.VehicleListings
                .Include(x => x.CarModel)
                .Include(x => x.ListingLocation)
                .Include(x => x.User)
                .ThenInclude(u => u.Location)
                .AsQueryable();

            // Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Color", StringComparison.OrdinalIgnoreCase))
                {
                    vehicleListings = vehicleListings.Where(x => x.Color.Contains(filterQuery));
                }
                else if (filterOn.Equals("Price", StringComparison.OrdinalIgnoreCase))
                {
                    // For price, try to parse the query as a double for exact or range matching
                    if (double.TryParse(filterQuery, out double priceValue))
                    {
                        vehicleListings = vehicleListings.Where(x => x.Price <= priceValue);
                    }
                }
            }

            // Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Color", StringComparison.OrdinalIgnoreCase))
                {
                    vehicleListings = isAscending ? vehicleListings.OrderBy(x => x.Color) : vehicleListings.OrderByDescending(x => x.Color);
                }
                else if (sortBy.Equals("Price", StringComparison.OrdinalIgnoreCase))
                {
                    vehicleListings = isAscending ? vehicleListings.OrderBy(x => x.Price) : vehicleListings.OrderByDescending(x => x.Price);
                }
            }

            // Pagination
            vehicleListings = vehicleListings.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return await vehicleListings.ToListAsync();
        }

        public async Task<VehicleListing?> GetVehicleListingByIdAsync(Guid id)
        {
            return await dbContext.VehicleListings
                .Include(x => x.CarModel)
                .Include(x => x.ListingLocation)
                .Include(x => x.User)
                .ThenInclude(u => u.Location)
                .FirstOrDefaultAsync(x => x.Id == id);
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
            existingVehicleListing.UserId = vehicleListing.UserId;

            await dbContext.SaveChangesAsync();
            return existingVehicleListing;
        }
    }
}
