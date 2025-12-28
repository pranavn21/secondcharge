using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using secondcharge.api.Data;
using secondcharge.api.Models.Domain;
using secondcharge.api.Models.DTO;
using secondcharge.api.Repositories.Interfaces;

namespace secondcharge.api.Controllers
{
    // https://localhost:portnumber/Users
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly SecondChargeDbContext dbContext;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UsersController(SecondChargeDbContext dbContext, IUserRepository userRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        // GET ALL USERS
        // GET: https://localhost:portnumber/api/users?filterOn=UserRole&filterQuery=Admin&sortBy=UserName&isAscending=true&pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery] string? filterOn, [FromQuery] string? filterQuery, 
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            // Get Data from database - Domain models
            var usersDomain = await userRepository.GetAllUsersAsync(filterOn, filterQuery, 
                sortBy, isAscending ?? true, pageNumber, pageSize);

            // Map domain to DTO & return DTOs
            return Ok(mapper.Map<List<UserDto>>(usersDomain));
        }

        // GET A USER BY ID
        // GET: https://localhost:portnumber/api/users/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            // Get User Domain Model from DB
            var userDomain = await userRepository.GetUserByIdAsync(id);
            if (userDomain == null)
            {
                return NotFound();
            }

            // Map/Convert User Domain Model to User DTO and return it
            return Ok(mapper.Map<UserDto>(userDomain));
        }

        // POST To Create New User
        // POST: https://localhost:portnumber/api/users
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddUserRequestDto addUserRequestDto)
        {
            // Map DTO to Domain Model
            var userDomainModel = mapper.Map<User>(addUserRequestDto);

            // Use Domain Model to create User
            userDomainModel = await userRepository.CreateAsync(userDomainModel);

            // Map Domain model back to DTO
            var userDto = mapper.Map<UserDto>(userDomainModel);

            // Using CreatedAtAction, we generate a 201 response that auto-generates a Location header to tell the client where to retrieve the new resource
            // first parameter is needed to get the action method to retrieve the resource, then we need to route value for that method, then response body
            return CreatedAtAction(nameof(GetUserById), new { id = userDto.Id }, userDto); // POST doesn't return 200, they should return 201
        }

        // Update user
        // PUT: https://localhost:portnumber/api/users/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateUserRequestDto updateUserRequestDto)
        {
            // Map DTO to Domain Model
            var userDomainModel = mapper.Map<User>(updateUserRequestDto);

            userDomainModel = await userRepository.UpdateAsync(id, userDomainModel);

            if (userDomainModel == null)
            {
                return NotFound();
            }

            // Map Domain model back to DTO and return it
            return Ok(mapper.Map<UserDto>(userDomainModel));
        }

        // Delete User
        // DELETE: https://localhost:portnumber/api/users/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var userDomainModel = await userRepository.DeleteAsync(id);

            if (userDomainModel == null)
            {
                return NotFound();
            }

            // return domainmodel to Dto and return it
            return Ok(mapper.Map<UserDto>(userDomainModel));
        }
    }
}
