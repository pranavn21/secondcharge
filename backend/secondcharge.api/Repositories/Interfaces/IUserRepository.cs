using secondcharge.api.Models.Domain;

namespace secondcharge.api.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync(string? filterOn = null, string? filterQuery = null, 
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000);
        Task<User?> GetUserByIdAsync(Guid id);
        Task<User> CreateAsync(User user);
        Task<User?> UpdateAsync(Guid id, User user);
        Task<User?> DeleteAsync(Guid id);
    }
}
