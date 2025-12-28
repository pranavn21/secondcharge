using secondcharge.api.Models.Domain;

namespace secondcharge.api.Repositories.Interfaces
{
    public interface ILocationRepository
    {
        Task<List<Location>> GetAllLocationsAsync(string? filterOn = null, string? filterQuery = null, 
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000);
        Task<Location?> GetLocationByIdAsync(Guid id);
        Task<Location> CreateAsync(Location location);
        Task<Location?> UpdateAsync(Guid id, Location location);
        Task<Location?> DeleteAsync(Guid id);
    }
}
