using Microsoft.EntityFrameworkCore;
using secondcharge.api.Data;
using secondcharge.api.Models.Domain;
using secondcharge.api.Repositories.Interfaces;

namespace secondcharge.api.Repositories.SQL
{
    public class SQLCarRepository : ICarRepository
    {
        private readonly SecondChargeDbContext dbContext;

        public SQLCarRepository(SecondChargeDbContext secondChargeDbContext)
        {
            this.dbContext = secondChargeDbContext;
        }

        public async Task<Car> CreateAsync(Car car)
        {
            await dbContext.Cars.AddAsync(car);
            await dbContext.SaveChangesAsync();
            return car;
        }

        public async Task<Car?> DeleteAsync(Guid id)
        {
            var existingCar = await dbContext.Cars.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCar == null)
            {
                return null;
            }

            dbContext.Cars.Remove(existingCar);

            await dbContext.SaveChangesAsync();
            return existingCar;
        }

        public async Task<List<Car>> GetAllCarsAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            var cars = dbContext.Cars.AsQueryable();

            // Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false) { 
                if (filterOn.Equals("Make", StringComparison.OrdinalIgnoreCase))
                {
                    cars = cars.Where(x => x.Make.Contains(filterQuery));
                }
                else if (filterOn.Equals("Model", StringComparison.OrdinalIgnoreCase))
                {
                    cars = cars.Where(x => x.Model.Contains(filterQuery));
                }
            }

            // Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Make", StringComparison.OrdinalIgnoreCase))
                {
                    cars = isAscending ? cars.OrderBy(x => x.Make) : cars.OrderByDescending(x => x.Make);
                }
                else if (sortBy.Equals("Model", StringComparison.OrdinalIgnoreCase))
                {
                    cars = isAscending ? cars.OrderBy(x => x.Model) : cars.OrderByDescending(x => x.Model);
                }
                else if (sortBy.Equals("Efficiency", StringComparison.OrdinalIgnoreCase))
                {
                    cars = isAscending ? cars.OrderBy(x => x.Efficiency) : cars.OrderByDescending(x => x.Efficiency);
                }
            }

            // Pagination
            cars = cars.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return await cars.ToListAsync();
        }

        public async Task<Car?> GetCarByIdAsync(Guid id)
        {
            return await dbContext.Cars.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Car?> UpdateAsync(Guid id, Car car)
        {
            var existingCar = await dbContext.Cars.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCar == null)
            {
                return null;
            }
            existingCar.Make = car.Make;
            existingCar.Model = car.Model;
            existingCar.Efficiency = car.Efficiency;
            existingCar.ModelImageUrl = car.ModelImageUrl;

            await dbContext.SaveChangesAsync();

            return existingCar;
        }
    }
}
