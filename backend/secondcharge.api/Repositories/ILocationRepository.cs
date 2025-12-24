using secondcharge.api.Models.Domain;

namespace secondcharge.api.Repositories
{
    public interface ILocationRepository
    {
        Task<List<Location>> GetAllLocationsAsync();
        Task<Location?> GetLocationByIdAsync(Guid id);
        Task<Location> CreateAsync(Location location);
        Task<Location?> UpdateAsync(Guid id, Location location);
        Task<Location?> DeleteAsync(Guid id);
    }
}
