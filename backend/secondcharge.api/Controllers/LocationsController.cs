using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using secondcharge.api.Data;
using secondcharge.api.Models.Domain;
using secondcharge.api.Models.DTO;
using secondcharge.api.Repositories;

namespace secondcharge.api.Controllers
{
    // https://localhost:portnumber/Locations
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly SecondChargeDbContext dbContext;
        private readonly ILocationRepository locationRepository;

        public LocationsController(SecondChargeDbContext dbContext, ILocationRepository locationRepository)
        {
            this.dbContext = dbContext;
            this.locationRepository = locationRepository;
        }

        // GET ALL LOCATIONS
        // GET: https://localhost:portnumber/api/locations
        [HttpGet]
        public async Task<IActionResult> GetAllLocations()
        {
            // Get Data from database - Domain models
            var locationsDomain = await locationRepository.GetAllLocationsAsync();

            // Map Domain Models to DTO
            var locationsDto = new List<LocationDto>();
            foreach (var locationDomain in locationsDomain)
            {
                locationsDto.Add(new LocationDto()
                {
                    Id = locationDomain.Id,
                    Country = locationDomain.Country,
                    State = locationDomain.State,
                    zipCode = locationDomain.zipCode
                });
            }

            // Return DTOs
            return Ok(locationsDto);
        }

        // GET A LOCATION BY ID
        // GET: https://localhost:portnumber/api/locations/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetLocationById(Guid id)
        {
            // Get Location Domain Model from DB
            var locationDomain = await locationRepository.GetLocationByIdAsync(id);
            if (locationDomain == null)
            {
                return NotFound();
            }

            // Map/Convert Location Domain Model to Location DTO
            var locationDto = new LocationDto
            {
                Id = locationDomain.Id,
                Country = locationDomain.Country,
                State = locationDomain.State,
                zipCode = locationDomain.zipCode
            };

            return Ok(locationDto);
        }

        // POST To Create New Location
        // POST: https://localhost:portnumber/api/locations
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddLocationRequestDto addLocationRequestDto)
        {
            // Map DTO to Domain Model
            var locationDomainModel = new Location
            {
                Country = addLocationRequestDto.Country,
                State = addLocationRequestDto.State,
                zipCode = addLocationRequestDto.zipCode
            };

            // Use Domain Model to create Location
            locationDomainModel = await locationRepository.CreateAsync(locationDomainModel);

            // Map Domain model back to DTO
            var locationDto = new LocationDto
            {
                Id = locationDomainModel.Id,
                Country = locationDomainModel.Country,
                State = locationDomainModel.State,
                zipCode = locationDomainModel.zipCode
            };

            // Using CreatedAtAction, we generate a 201 response that auto-generates a Location header to tell the client where to retrieve the new resource
            // first parameter is needed to get the action method to retrieve the resource, then we need to route value for that method, then response body
            return CreatedAtAction(nameof(GetLocationById), new { id = locationDto.Id }, locationDto); // POST doesn't return 200, they should return 201
        }

        // Update location
        // PUT: https://localhost:portnumber/api/locations/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateLocationRequestDto updateLocationRequestDto)
        {
            // Map DTO to Domain Model
            var locationDomainModel = new Location
            {
                Country = updateLocationRequestDto.Country,
                State = updateLocationRequestDto.State,
                zipCode = updateLocationRequestDto.zipCode
            };

            locationDomainModel = await locationRepository.UpdateAsync(id, locationDomainModel);

            if (locationDomainModel == null)
            {
                return NotFound();
            }

            // Map Domain model back to DTO
            var locationDto = new LocationDto
            {
                Id = locationDomainModel.Id,
                Country = locationDomainModel.Country,
                State = locationDomainModel.State,
                zipCode = locationDomainModel.zipCode
            };

            return Ok(locationDto);
        }

        // Delete Location
        // DELETE: https://localhost:portnumber/api/locations/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var locationDomainModel = await locationRepository.DeleteAsync(id);

            if (locationDomainModel == null)
            {
                return NotFound();
            }

            // return domainmodel to Dto
            var locationDto = new LocationDto
            {
                Id = locationDomainModel.Id,
                Country = locationDomainModel.Country,
                State = locationDomainModel.State,
                zipCode = locationDomainModel.zipCode
            };

            return Ok(locationDto);
        }
    }
}
