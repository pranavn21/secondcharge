using Microsoft.EntityFrameworkCore;
using secondcharge.api.Data;
using secondcharge.api.Models.Domain;
using secondcharge.api.Repositories.Interfaces;

namespace secondcharge.api.Repositories.SQL
{
    public class SQLUserRepository : IUserRepository
    {
        private readonly SecondChargeDbContext dbContext;

        public SQLUserRepository(SecondChargeDbContext secondChargeDbContext)
        {
            this.dbContext = secondChargeDbContext;
        }

        public async Task<User> CreateAsync(User user)
        {
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User?> DeleteAsync(Guid id)
        {
            var existingUser = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (existingUser == null)
            {
                return null;
            }

            dbContext.Users.Remove(existingUser);
            await dbContext.SaveChangesAsync();
            return existingUser;
        }

        public async Task<List<User>> GetAllUsersAsync(string? filterOn = null, string? filterQuery = null, 
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            var users = dbContext.Users.Include(x => x.Location).AsQueryable();

            // Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("UserName", StringComparison.OrdinalIgnoreCase))
                {
                    users = users.Where(x => x.UserName.Contains(filterQuery));
                }
                else if (filterOn.Equals("UserRole", StringComparison.OrdinalIgnoreCase))
                {
                    users = users.Where(x => x.UserRole.Contains(filterQuery));
                }
            }

            // Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("UserName", StringComparison.OrdinalIgnoreCase))
                {
                    users = isAscending ? users.OrderBy(x => x.UserName) : users.OrderByDescending(x => x.UserName);
                }
                else if (sortBy.Equals("UserRole", StringComparison.OrdinalIgnoreCase))
                {
                    users = isAscending ? users.OrderBy(x => x.UserRole) : users.OrderByDescending(x => x.UserRole);
                }
            }

            // Pagination
            users = users.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return await users.ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await dbContext.Users.Include("Location").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User?> UpdateAsync(Guid id, User user)
        {
            var existingUser = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (existingUser == null)
            {
                return null;
            }
            existingUser.UserName = user.UserName;
            existingUser.Password = user.Password;
            existingUser.LocationId = user.LocationId;
            existingUser.UserRole = user.UserRole;
            existingUser.UserPhoneNumber = user.UserPhoneNumber;

            await dbContext.SaveChangesAsync();
            return existingUser;
        }
    }
}
