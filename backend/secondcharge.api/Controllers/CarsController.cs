using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using secondcharge.api.Data;
using secondcharge.api.Models.Domain;
using secondcharge.api.Models.DTO;
using secondcharge.api.Repositories.Interfaces;

namespace secondcharge.api.Controllers
{
    // https://localhost:portnumber/Cars
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly SecondChargeDbContext dbContext;
        private readonly ICarRepository carRepository;
        private readonly IMapper mapper;

        public CarsController(SecondChargeDbContext dbContext, ICarRepository carRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.carRepository = carRepository;
            this.mapper = mapper;
        }

        // GET ALL CARS
        // GET: https://localhost:portnumber/api/cars
        [HttpGet]
        public async Task<IActionResult> GetAllCars()
        {
            // Get Data from database - Domain models
            var carsDomain = await carRepository.GetAllCarsAsync();

            // Map domain to DTO & return DTOs
            return Ok(mapper.Map<List<CarDto>>(carsDomain));
        }

        // GET A CAR BY ID
        // GET: https://localhost:portnumber/api/cars/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCarById(Guid id)
        {
            //var car = dbContext.Cars.Find(id);
            // Get Car Domain Model from DB
            var carDomain = await carRepository.GetCarByIdAsync(id);
            if (carDomain == null)
            {
                return NotFound();
            }

            // Map/Convert Car Domain Model to Car DTO and return it
            return Ok(mapper.Map<CarDto>(carDomain));
        }

        // POST To Create New Car
        // POST: https://localhost:portnumber/api/cars
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddCarRequestDto addCarRequestDto)
        {
            // Map DTO to Domain Model
            var carDomainModel = mapper.Map<Car>(addCarRequestDto);

            // Use Domain Model to create Cars
            carDomainModel = await carRepository.CreateAsync(carDomainModel);

            // Map Domain model back to DTO
            var carDto = mapper.Map<CarDto>(carDomainModel);

            // Using CreatedAtAction, we generate a 201 response that auto-generates a Location header to tell the client where to retrieve the new resource
            // first parameter is needed to get the action method to retrieve the resource, then we need to route value for that method, then response body
            return CreatedAtAction(nameof(GetCarById), new { id = carDto.Id }, carDto); // POST doesn't return 200, they should return 201
        }

        // Update cars
        // PUT: https://localhost:portnumber/api/cars
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCarRequestDto updateCarRequestDto)
        {
            // Map DTO to Domain Model
            var carDomainModel = mapper.Map<Car>(updateCarRequestDto);

            carDomainModel = await carRepository.UpdateAsync(id, carDomainModel);

            if (carDomainModel == null)
            {
                return NotFound();
            }

            // Map Domain model back to DTO and return it
            return Ok(mapper.Map<CarDto>(carDomainModel));
        }

        // Delete Car
        // DELETE: https://localhost:portnumber/api/cars/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var carDomainModel = await carRepository.DeleteAsync(id);

            if (carDomainModel == null)
            {
                return NotFound();
            }

            // return domainmodel to Dto and return it
            return Ok(mapper.Map<CarDto>(carDomainModel));
        }
    }
}
