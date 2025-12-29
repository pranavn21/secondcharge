using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using secondcharge.api.CustomActionFilters;
using secondcharge.api.Data;
using secondcharge.api.Models.Domain;
using secondcharge.api.Models.DTO.Location;
using secondcharge.api.Repositories.Interfaces;

namespace secondcharge.api.Controllers
{
    // https://localhost:portnumber/Locations
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LocationsController : ControllerBase
    {
        private readonly SecondChargeDbContext dbContext;
        private readonly ILocationRepository locationRepository;
        private readonly IMapper mapper;

        public LocationsController(SecondChargeDbContext dbContext, ILocationRepository locationRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.locationRepository = locationRepository;
            this.mapper = mapper;
        }

        // GET ALL LOCATIONS
        // GET: https://localhost:portnumber/api/locations?filterOn=Country&filterQuery=USA&sortBy=State&isAscending=true&pageNumber=1&pageSize=10
        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAllLocations([FromQuery] string? filterOn, [FromQuery] string? filterQuery, 
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            // Get Data from database - Domain models
            var locationsDomain = await locationRepository.GetAllLocationsAsync(filterOn, filterQuery, 
                sortBy, isAscending ?? true, pageNumber, pageSize);

            // Map domain to DTO & return DTOs
            return Ok(mapper.Map<List<LocationDto>>(locationsDomain));
        }

        // GET A LOCATION BY ID
        // GET: https://localhost:portnumber/api/locations/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetLocationById(Guid id)
        {
            // Get Location Domain Model from DB
            var locationDomain = await locationRepository.GetLocationByIdAsync(id);
            if (locationDomain == null)
            {
                return NotFound();
            }

            // Map/Convert Location Domain Model to Location DTO and return it
            return Ok(mapper.Map<LocationDto>(locationDomain));
        }

        // POST To Create New Location
        // POST: https://localhost:portnumber/api/locations
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddLocationRequestDto addLocationRequestDto)
        {
            // Map DTO to Domain Model
            var locationDomainModel = mapper.Map<Location>(addLocationRequestDto);

            // Use Domain Model to create Location
            locationDomainModel = await locationRepository.CreateAsync(locationDomainModel);

            // Map Domain model back to DTO
            var locationDto = mapper.Map<LocationDto>(locationDomainModel);

            // Using CreatedAtAction, we generate a 201 response that auto-generates a Location header to tell the client where to retrieve the new resource
            // first parameter is needed to get the action method to retrieve the resource, then we need to route value for that method, then response body
            return CreatedAtAction(nameof(GetLocationById), new { id = locationDto.Id }, locationDto); // POST doesn't return 200, they should return 201
        }

        // Update location
        // PUT: https://localhost:portnumber/api/locations/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateLocationRequestDto updateLocationRequestDto)
        {
            // Map DTO to Domain Model
            var locationDomainModel = mapper.Map<Location>(updateLocationRequestDto);

            locationDomainModel = await locationRepository.UpdateAsync(id, locationDomainModel);

            if (locationDomainModel == null)
            {
                return NotFound();
            }

            // Map Domain model back to DTO and return it
            return Ok(mapper.Map<LocationDto>(locationDomainModel));
        }

        // Delete Location
        // DELETE: https://localhost:portnumber/api/locations/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var locationDomainModel = await locationRepository.DeleteAsync(id);

            if (locationDomainModel == null)
            {
                return NotFound();
            }

            // return domainmodel to Dto and return it
            return Ok(mapper.Map<LocationDto>(locationDomainModel));
        }
    }
}
