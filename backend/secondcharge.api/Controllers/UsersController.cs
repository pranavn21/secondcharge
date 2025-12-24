using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using secondcharge.api.Data;
using secondcharge.api.Models.Domain;
using secondcharge.api.Models.DTO;

namespace secondcharge.api.Controllers
{
    // https://localhost:portnumber/Users
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly SecondChargeDbContext dbContext;

        public UsersController(SecondChargeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET ALL USERS
        // GET: https://localhost:portnumber/api/users
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            // Get Data from database - Domain models
            var usersDomain = await dbContext.Users.ToListAsync();

            // Map Domain Models to DTO
            var usersDto = new List<UserDto>();
            foreach (var userDomain in usersDomain)
            {
                usersDto.Add(new UserDto()
                {
                    Id = userDomain.Id,
                    UserName = userDomain.UserName,
                    Password = userDomain.Password,
                    userLocationId = userDomain.userLocationId,
                    UserRole = userDomain.UserRole,
                    UserPhoneNumber = userDomain.UserPhoneNumber
                });
            }

            // Return DTOs
            return Ok(usersDto);
        }

        // GET A USER BY ID
        // GET: https://localhost:portnumber/api/users/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            // Get User Domain Model from DB
            var userDomain = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (userDomain == null)
            {
                return NotFound();
            }

            // Map/Convert User Domain Model to User DTO
            var userDto = new UserDto
            {
                Id = userDomain.Id,
                UserName = userDomain.UserName,
                Password = userDomain.Password,
                userLocationId = userDomain.userLocationId,
                UserRole = userDomain.UserRole,
                UserPhoneNumber = userDomain.UserPhoneNumber
            };

            return Ok(userDto);
        }

        // POST To Create New User
        // POST: https://localhost:portnumber/api/users
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddUserRequestDto addUserRequestDto)
        {
            // Map DTO to Domain Model
            var userDomainModel = new User
            {
                UserName = addUserRequestDto.UserName,
                Password = addUserRequestDto.Password,
                userLocationId = addUserRequestDto.userLocationId,
                UserRole = addUserRequestDto.UserRole,
                UserPhoneNumber = addUserRequestDto.UserPhoneNumber
            };

            // Use Domain Model to create User
            await dbContext.Users.AddAsync(userDomainModel); // Track changes, not save
            await dbContext.SaveChangesAsync(); // This is needed to actually save the changes to the DB

            // Map Domain model back to DTO
            var userDto = new UserDto
            {
                Id = userDomainModel.Id,
                UserName = userDomainModel.UserName,
                Password = userDomainModel.Password,
                userLocationId = userDomainModel.userLocationId,
                UserRole = userDomainModel.UserRole,
                UserPhoneNumber = userDomainModel.UserPhoneNumber
            };

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
            var userDomainModel = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (userDomainModel == null)
            {
                return NotFound();
            }

            // Map DTO to Domain Model
            userDomainModel.UserName = updateUserRequestDto.UserName;
            userDomainModel.Password = updateUserRequestDto.Password;
            userDomainModel.userLocationId = updateUserRequestDto.userLocationId;
            userDomainModel.UserRole = updateUserRequestDto.UserRole;
            userDomainModel.UserPhoneNumber = updateUserRequestDto.UserPhoneNumber;

            await dbContext.SaveChangesAsync(); // don't need to add/update anything to the domain model as the domain model is being tracked, so we just need to save the changes

            var userDto = new UserDto
            {
                Id = userDomainModel.Id,
                UserName = userDomainModel.UserName,
                Password = userDomainModel.Password,
                userLocationId = userDomainModel.userLocationId,
                UserRole = userDomainModel.UserRole,
                UserPhoneNumber = userDomainModel.UserPhoneNumber
            };

            return Ok(userDto);
        }

        // Delete User
        // DELETE: https://localhost:portnumber/api/users/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var userDomainModel = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (userDomainModel == null)
            {
                return NotFound();
            }

            // Delete user
            dbContext.Users.Remove(userDomainModel); // there's no async version of Remove
            await dbContext.SaveChangesAsync();

            // return domainmodel to Dto
            var userDto = new UserDto
            {
                Id = userDomainModel.Id,
                UserName = userDomainModel.UserName,
                Password = userDomainModel.Password,
                userLocationId = userDomainModel.userLocationId,
                UserRole = userDomainModel.UserRole,
                UserPhoneNumber = userDomainModel.UserPhoneNumber
            };

            return Ok(userDto);
        }
    }
}
