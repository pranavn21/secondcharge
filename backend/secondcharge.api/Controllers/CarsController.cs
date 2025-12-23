using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using secondcharge.api.Data;
using secondcharge.api.Models.Domain;
using secondcharge.api.Models.DTO;

namespace secondcharge.api.Controllers
{
    // https://localhost:portnumber/Cars
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly SecondChargeDbContext dbContext;

        public CarsController(SecondChargeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET ALL CARS
        // GET: https://localhost:portnumber/api/cars
        [HttpGet]
        public IActionResult GetAllCars()
        {
            // Get Data from database - Domain models
            var carsDomain = dbContext.Cars.ToList();

            // Map Domain Models to DTO
            var carsDto = new List<CarDto>();
            foreach (var carDomain in carsDomain)
            {
                carsDto.Add(new CarDto()
                {
                    Id = carDomain.Id,
                    Make = carDomain.Make,
                    Model = carDomain.Model,
                    Efficiency = carDomain.Efficiency,
                    ModelImageUrl = carDomain.ModelImageUrl
                });
            }

            // Return DTOs
            return Ok(carsDto);
        }

        // GET A CAR BY ID
        // GET: https://localhost:portnumber/api/cars/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetCarById(Guid id)
        {
            //var car = dbContext.Cars.Find(id);
            // Get Car Domain Model from DB
            var carDomain = dbContext.Cars.FirstOrDefault(x => x.Id == id);
            if (carDomain == null)
            {
                return NotFound();
            }

            // Map/Convert Car Domain Model to Car DTO
            var carDto = new CarDto
            {
                Id = carDomain.Id,
                Make = carDomain.Make,
                Model = carDomain.Model,
                Efficiency = carDomain.Efficiency,
                ModelImageUrl = carDomain.ModelImageUrl
            };

            return Ok(carDto);
        }

        // POST To Create New Car
        // POST: https://localhost:portnumber/api/cars
        [HttpPost]
        public IActionResult Create([FromBody] AddCarRequestDto addCarRequestDto)
        {
            // Map DTO to Domain Model
            var carDomainModel = new Car
            {
                Make = addCarRequestDto.Make,
                Model = addCarRequestDto.Model,
                Efficiency = addCarRequestDto.Efficiency,
                ModelImageUrl = addCarRequestDto.ModelImageUrl
            };

            // Use Domain Model to create Region
            dbContext.Cars.Add(carDomainModel); // Track changes, not save
            dbContext.SaveChanges(); // This is needed to actually save the changes to the DB

            // Map Domain model back to DTO
            var carDto = new CarDto
            {
                Id = carDomainModel.Id,
                Make = carDomainModel.Make,
                Model = carDomainModel.Model,
                Efficiency = carDomainModel.Efficiency,
                ModelImageUrl = carDomainModel.ModelImageUrl
            };

            // Using CreatedAtAction, we generate a 201 response that auto-generates a Location header to tell the client where to retrieve the new resource
            // first parameter is needed to get the action method to retrieve the resource, then we need to route value for that method, then response body
            return CreatedAtAction(nameof(GetCarById), new { id = carDto.Id }, carDto); // POST doesn't return 200, they should return 201
        }

        // Update region
        // PUT: https://localhost:portnumber/api/cars
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateCarRequestDto updateCarRequestDto)
        {
            var carDomainModel = dbContext.Cars.FirstOrDefault(x => x.Id == id);

            if (carDomainModel == null)
            {
                return NotFound();
            }

            // Map DTO to Domain Model
            carDomainModel.Make = updateCarRequestDto.Make;
            carDomainModel.Model = updateCarRequestDto.Model;
            carDomainModel.Efficiency = updateCarRequestDto.Efficiency;
            carDomainModel.ModelImageUrl = updateCarRequestDto.ModelImageUrl;

            dbContext.SaveChanges(); // don't need to add/update anything to the domain model as the domain model is being tracked, so we just need to save the changes

            var carDto = new CarDto
            {
                Id = carDomainModel.Id,
                Make = carDomainModel.Make,
                Model = carDomainModel.Model,
                Efficiency = carDomainModel.Efficiency,
                ModelImageUrl = carDomainModel.ModelImageUrl
            };

            return Ok(carDto);
        }

        // Delete Region
        // DELETE: https://localhost:portnumber/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var carDomainModel = dbContext.Cars.FirstOrDefault(x => x.Id == id);

            if (carDomainModel == null)
            {
                return NotFound();
            }

            // Delete region
            dbContext.Cars.Remove(carDomainModel);
            dbContext.SaveChanges();

            // return domainmodel to Dto
            var carDto = new CarDto
            {
                Id = carDomainModel.Id,
                Make = carDomainModel.Make,
                Model = carDomainModel.Model,
                Efficiency = carDomainModel.Efficiency,
                ModelImageUrl = carDomainModel.ModelImageUrl
            };

            return Ok(carDto);
        }
    }


}
