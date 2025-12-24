using secondcharge.api.Models.Domain;

namespace secondcharge.api.Repositories.Interfaces
{
    public interface IVehicleListingRepository
    {
        Task<List<VehicleListing>> GetAllVehicleListingsAsync();
        Task<VehicleListing?> GetVehicleListingByIdAsync(Guid id);
        Task<VehicleListing> CreateAsync(VehicleListing vehicleListing);
        Task<VehicleListing?> UpdateAsync(Guid id, VehicleListing vehicleListing);
        Task<VehicleListing?> DeleteAsync(Guid id);
    }
}
