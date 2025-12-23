using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using secondcharge.api.Data;
using secondcharge.api.Models.Domain;
using secondcharge.api.Models.DTO;

namespace secondcharge.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleListingsController : ControllerBase
    {
        private readonly SecondChargeDbContext dbContext;

        public VehicleListingsController(SecondChargeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET ALL VEHICLE LISTINGS
        // GET: https://localhost:portnumber/api/vehiclelistings
        [HttpGet]
        public IActionResult GetAllListings()
        {
            // Get Data from database - Domain models
            var vehicleListingsDomain = dbContext.VehicleListings.ToList();

            // Map Domain Models to DTO
            var vehicleListingsDto = new List<VehicleListingDto>();
            foreach (var vehicleListingDomain in vehicleListingsDomain)
            {
                vehicleListingsDto.Add(new VehicleListingDto()
                {
                    Id = vehicleListingDomain.Id,
                    CarId = vehicleListingDomain.CarId,
                    Mileage = vehicleListingDomain.Mileage,
                    Color = vehicleListingDomain.Color,
                    listingLocationId = vehicleListingDomain.listingLocationId,
                    Price = vehicleListingDomain.Price
                });
            }

            // Return DTOs
            return Ok(vehicleListingsDto);
        }

        // GET A VEHICLE LISTING BY ID
        // GET: https://localhost:portnumber/api/vehiclelistings/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetVehicleListingById(Guid id)
        {
            // Get Vehicle Listing Domain Model from DB
            var vehicleListingDomain = dbContext.VehicleListings.FirstOrDefault(x => x.Id == id);
            if (vehicleListingDomain == null)
            {
                return NotFound();
            }

            // Map/Convert Vehicle Listing Domain Model to Vehicle Listing DTO
            var vehicleListingDto = new VehicleListingDto
            {
                Id = vehicleListingDomain.Id,
                CarId = vehicleListingDomain.CarId,
                Mileage = vehicleListingDomain.Mileage,
                Color = vehicleListingDomain.Color,
                listingLocationId = vehicleListingDomain.listingLocationId,
                Price = vehicleListingDomain.Price
            };

            return Ok(vehicleListingDto);
        }

        // POST To Create New Vehicle Listing
        // POST: https://localhost:portnumber/api/vehiclelistings
        [HttpPost]
        public IActionResult Create([FromBody] AddVehicleListingRequestDto addVehicleListingRequestDto)
        {
            // Map DTO to Domain Model
            var vehicleListingDomainModel = new VehicleListing
            {
                CarId = addVehicleListingRequestDto.CarId,
                Mileage = addVehicleListingRequestDto.Mileage,
                Color = addVehicleListingRequestDto.Color,
                listingLocationId = addVehicleListingRequestDto.listingLocationId,
                Price = addVehicleListingRequestDto.Price
            };

            // Use Domain Model to create Vehicle Listing
            dbContext.VehicleListings.Add(vehicleListingDomainModel); // Track changes, not save
            dbContext.SaveChanges(); // This is needed to actually save the changes to the DB

            // Map Domain model back to DTO
            var vehicleListingDto = new VehicleListingDto
            {
                Id = vehicleListingDomainModel.Id,
                CarId = vehicleListingDomainModel.CarId,
                Mileage = vehicleListingDomainModel.Mileage,
                Color = vehicleListingDomainModel.Color,
                listingLocationId = vehicleListingDomainModel.listingLocationId,
                Price = vehicleListingDomainModel.Price
            };

            // Using CreatedAtAction, we generate a 201 response that auto-generates a Location header to tell the client where to retrieve the new resource
            // first parameter is needed to get the action method to retrieve the resource, then we need to route value for that method, then response body
            return CreatedAtAction(nameof(GetVehicleListingById), new { id = vehicleListingDto.Id }, vehicleListingDto); // POST doesn't return 200, they should return 201
        }

        // Update Vehicle Listing
        // PUT: https://localhost:portnumber/api/vehiclelistings/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateVehicleListingRequestDto updateVehicleListingRequestDto)
        {
            var vehicleListingDomainModel = dbContext.VehicleListings.FirstOrDefault(x => x.Id == id);

            if (vehicleListingDomainModel == null)
            {
                return NotFound();
            }

            // Map DTO to Domain Model
            vehicleListingDomainModel.CarId = updateVehicleListingRequestDto.CarId;
            vehicleListingDomainModel.Mileage = updateVehicleListingRequestDto.Mileage;
            vehicleListingDomainModel.Color = updateVehicleListingRequestDto.Color;
            vehicleListingDomainModel.listingLocationId = updateVehicleListingRequestDto.listingLocationId;
            vehicleListingDomainModel.Price = updateVehicleListingRequestDto.Price;

            dbContext.SaveChanges(); // don't need to add/update anything to the domain model as the domain model is being tracked, so we just need to save the changes

            var vehicleListingDto = new VehicleListingDto
            {
                Id = vehicleListingDomainModel.Id,
                CarId = vehicleListingDomainModel.CarId,
                Mileage = vehicleListingDomainModel.Mileage,
                Color = vehicleListingDomainModel.Color,
                listingLocationId = vehicleListingDomainModel.listingLocationId,
                Price = vehicleListingDomainModel.Price
            };

            return Ok(vehicleListingDto);
        }

        // Delete Vehicle Listing
        // DELETE: https://localhost:portnumber/api/vehiclelistings/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var vehicleListingDomainModel = dbContext.VehicleListings.FirstOrDefault(x => x.Id == id);

            if (vehicleListingDomainModel == null)
            {
                return NotFound();
            }

            // Delete vehicle listing
            dbContext.VehicleListings.Remove(vehicleListingDomainModel);
            dbContext.SaveChanges();

            // return domainmodel to Dto
            var vehicleListingDto = new VehicleListingDto
            {
                Id = vehicleListingDomainModel.Id,
                CarId = vehicleListingDomainModel.CarId,
                Mileage = vehicleListingDomainModel.Mileage,
                Color = vehicleListingDomainModel.Color,
                listingLocationId = vehicleListingDomainModel.listingLocationId,
                Price = vehicleListingDomainModel.Price
            };

            return Ok(vehicleListingDto);
        }
    }
}
