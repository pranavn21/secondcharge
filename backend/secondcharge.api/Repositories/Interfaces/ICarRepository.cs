using secondcharge.api.Models.Domain;

namespace secondcharge.api.Repositories.Interfaces
{
    public interface ICarRepository
    {
        Task<List<Car>> GetAllCarsAsync(string? filterOn = null, string? filterQuery = null);
        Task<Car?> GetCarByIdAsync(Guid id);
        Task<Car> CreateAsync(Car car);
        Task<Car?> UpdateAsync(Guid id, Car car);
        Task<Car?> DeleteAsync(Guid id);  
    }
}
