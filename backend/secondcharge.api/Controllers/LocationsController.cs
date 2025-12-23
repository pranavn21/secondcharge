using Microsoft.AspNetCore.Mvc;
using secondcharge.api.Data;
using secondcharge.api.Models.Domain;
using secondcharge.api.Models.DTO;

namespace secondcharge.api.Controllers
{
    // https://localhost:portnumber/Locations
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly SecondChargeDbContext dbContext;

        public LocationsController(SecondChargeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET ALL LOCATIONS
        // GET: https://localhost:portnumber/api/locations
        [HttpGet]
        public IActionResult GetAllLocations()
        {
            // Get Data from database - Domain models
            var locationsDomain = dbContext.Locations.ToList();

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
        public IActionResult GetLocationById(Guid id)
        {
            // Get Location Domain Model from DB
            var locationDomain = dbContext.Locations.FirstOrDefault(x => x.Id == id);
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
        public IActionResult Create([FromBody] AddLocationRequestDto addLocationRequestDto)
        {
            // Map DTO to Domain Model
            var locationDomainModel = new Location
            {
                Country = addLocationRequestDto.Country,
                State = addLocationRequestDto.State,
                zipCode = addLocationRequestDto.zipCode
            };

            // Use Domain Model to create Location
            dbContext.Locations.Add(locationDomainModel); // Track changes, not save
            dbContext.SaveChanges(); // This is needed to actually save the changes to the DB

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
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateLocationRequestDto updateLocationRequestDto)
        {
            var locationDomainModel = dbContext.Locations.FirstOrDefault(x => x.Id == id);

            if (locationDomainModel == null)
            {
                return NotFound();
            }

            // Map DTO to Domain Model
            locationDomainModel.Country = updateLocationRequestDto.Country;
            locationDomainModel.State = updateLocationRequestDto.State;
            locationDomainModel.zipCode = updateLocationRequestDto.zipCode;

            dbContext.SaveChanges(); // don't need to add/update anything to the domain model as the domain model is being tracked, so we just need to save the changes

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
        public IActionResult Delete([FromRoute] Guid id)
        {
            var locationDomainModel = dbContext.Locations.FirstOrDefault(x => x.Id == id);

            if (locationDomainModel == null)
            {
                return NotFound();
            }

            // Delete location
            dbContext.Locations.Remove(locationDomainModel);
            dbContext.SaveChanges();

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
