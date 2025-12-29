using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using secondcharge.api.CustomActionFilters;
using secondcharge.api.Data;
using secondcharge.api.Models.Domain;
using secondcharge.api.Models.DTO.VehicleListing;
using secondcharge.api.Repositories.Interfaces;

namespace secondcharge.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VehicleListingsController : ControllerBase
    {
        private readonly SecondChargeDbContext dbContext;
        private readonly IVehicleListingRepository vehicleListingRepository;
        private readonly IMapper mapper;

        public VehicleListingsController(SecondChargeDbContext dbContext, IVehicleListingRepository vehicleListingRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.vehicleListingRepository = vehicleListingRepository;
            this.mapper = mapper;
        }

        // GET ALL VEHICLE LISTINGS
        // GET: https://localhost:portnumber/api/vehiclelistings?filterOn=Color&filterQuery=Blue&sortBy=Price&isAscending=true&pageNumber=1&pageSize=10
        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAllListings([FromQuery] string? filterOn, [FromQuery] string? filterQuery, 
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            // Get Data from database - Domain models
            var vehicleListingsDomain = await vehicleListingRepository.GetAllVehicleListingsAsync(filterOn, filterQuery, 
                sortBy, isAscending ?? true, pageNumber, pageSize);

            // Map domain to DTO & return DTOs
            return Ok(mapper.Map<List<VehicleListingDto>>(vehicleListingsDomain));
        }

        // GET A VEHICLE LISTING BY ID
        // GET: https://localhost:portnumber/api/vehiclelistings/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetVehicleListingById(Guid id)
        {
            // Get Vehicle Listing Domain Model from DB
            var vehicleListingDomain = await vehicleListingRepository.GetVehicleListingByIdAsync(id);
            if (vehicleListingDomain == null)
            {
                return NotFound();
            }

            // Map/Convert Vehicle Listing Domain Model to Vehicle Listing DTO and return it
            return Ok(mapper.Map<VehicleListingDto>(vehicleListingDomain));
        }

        // POST To Create New Vehicle Listing
        // POST: https://localhost:portnumber/api/vehiclelistings
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddVehicleListingRequestDto addVehicleListingRequestDto)
        {
            // Map DTO to Domain Model
            var vehicleListingDomainModel = mapper.Map<VehicleListing>(addVehicleListingRequestDto);

            // Use Domain Model to create Vehicle Listing
            vehicleListingDomainModel = await vehicleListingRepository.CreateAsync(vehicleListingDomainModel);

            // Map Domain model back to DTO
            var vehicleListingDto = mapper.Map<VehicleListingDto>(vehicleListingDomainModel);

            // Using CreatedAtAction, we generate a 201 response that auto-generates a Location header to tell the client where to retrieve the new resource
            // first parameter is needed to get the action method to retrieve the resource, then we need to route value for that method, then response body
            return CreatedAtAction(nameof(GetVehicleListingById), new { id = vehicleListingDto.Id }, vehicleListingDto); // POST doesn't return 200, they should return 201
        }

        // Update Vehicle Listing
        // PUT: https://localhost:portnumber/api/vehiclelistings/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateVehicleListingRequestDto updateVehicleListingRequestDto)
        {
            // Map DTO to Domain Model
            var vehicleListingDomainModel = mapper.Map<VehicleListing>(updateVehicleListingRequestDto);

            vehicleListingDomainModel = await vehicleListingRepository.UpdateAsync(id, vehicleListingDomainModel);

            if (vehicleListingDomainModel == null)
            {
                return NotFound();
            }

            // Map Domain model back to DTO and return it
            return Ok(mapper.Map<VehicleListingDto>(vehicleListingDomainModel));
        }

        // Delete Vehicle Listing
        // DELETE: https://localhost:portnumber/api/vehiclelistings/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var vehicleListingDomainModel = await vehicleListingRepository.DeleteAsync(id);

            if (vehicleListingDomainModel == null)
            {
                return NotFound();
            }

            // return domainmodel to Dto and return it
            return Ok(mapper.Map<VehicleListingDto>(vehicleListingDomainModel));
        }
    }
}
