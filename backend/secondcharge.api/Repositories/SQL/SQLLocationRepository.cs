using Microsoft.EntityFrameworkCore;
using secondcharge.api.Data;
using secondcharge.api.Models.Domain;
using secondcharge.api.Repositories.Interfaces;

namespace secondcharge.api.Repositories.SQL
{
    public class SQLLocationRepository : ILocationRepository
    {
        private readonly SecondChargeDbContext dbContext;

        public SQLLocationRepository(SecondChargeDbContext secondChargeDbContext)
        {
            this.dbContext = secondChargeDbContext;
        }

        public async Task<Location> CreateAsync(Location location)
        {
            await dbContext.Locations.AddAsync(location);
            await dbContext.SaveChangesAsync();
            return location;
        }

        public async Task<Location?> DeleteAsync(Guid id)
        {
            var existingLocation = await dbContext.Locations.FirstOrDefaultAsync(x => x.Id == id);
            if (existingLocation == null)
            {
                return null;
            }

            dbContext.Locations.Remove(existingLocation);
            await dbContext.SaveChangesAsync();
            return existingLocation;
        }

        public async Task<List<Location>> GetAllLocationsAsync(string? filterOn = null, string? filterQuery = null, 
            string? sortBy = null, bool isAscending = true)
        {
            var locations = dbContext.Locations.AsQueryable();

            // Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Country", StringComparison.OrdinalIgnoreCase))
                {
                    locations = locations.Where(x => x.Country.Contains(filterQuery));
                }
                else if (filterOn.Equals("State", StringComparison.OrdinalIgnoreCase))
                {
                    locations = locations.Where(x => x.State.Contains(filterQuery));
                }
                else if (filterOn.Equals("zipCode", StringComparison.OrdinalIgnoreCase))
                {
                    locations = locations.Where(x => x.zipCode.Contains(filterQuery));
                }
            }

            // Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Country", StringComparison.OrdinalIgnoreCase))
                {
                    locations = isAscending ? locations.OrderBy(x => x.Country) : locations.OrderByDescending(x => x.Country);
                }
                else if (sortBy.Equals("State", StringComparison.OrdinalIgnoreCase))
                {
                    locations = isAscending ? locations.OrderBy(x => x.State) : locations.OrderByDescending(x => x.State);
                }
                else if (sortBy.Equals("zipCode", StringComparison.OrdinalIgnoreCase))
                {
                    locations = isAscending ? locations.OrderBy(x => x.zipCode) : locations.OrderByDescending(x => x.zipCode);
                }
            }

            return await locations.ToListAsync();
        }

        public async Task<Location?> GetLocationByIdAsync(Guid id)
        {
            return await dbContext.Locations.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Location?> UpdateAsync(Guid id, Location location)
        {
            var existingLocation = await dbContext.Locations.FirstOrDefaultAsync(x => x.Id == id);
            if (existingLocation == null)
            {
                return null;
            }
            existingLocation.Country = location.Country;
            existingLocation.State = location.State;
            existingLocation.zipCode = location.zipCode;

            await dbContext.SaveChangesAsync();
            return existingLocation;
        }
    }
}
