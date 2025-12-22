using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace secondcharge.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleListingsController : ControllerBase
    {
        // GET: https://localhost:portnumber/api/students
        [HttpGet]
        public IActionResult VehicleListings()
        {
            string[] listings = new string[] { "2020 Tesla Cybertruck", "2025 Lucid Air Sapphire" };
            return Ok(listings);    
        }
    }
}
