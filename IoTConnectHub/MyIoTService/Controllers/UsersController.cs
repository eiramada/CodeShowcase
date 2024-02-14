using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyIoTService.Data.Data;
using MyIoTService.Data.Entities;
using MyIoTService.DTO;

namespace MyIoTService.Controllers
{
    /// <summary>
    /// Controller for managing user-related actions.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly MyIoTDataContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="context">The database context used for accessing IoT users.</param>
        public UsersController(MyIoTDataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Registers a new user with the provided details.
        /// </summary>
        /// <param name="userDto">The user data transfer object containing the username and password.</param>
        /// <response code="200">Returns the user ID of the newly created user.</response>
        /// <response code="400">Returned if the provided data is invalid or if the registration fails.</response>
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDTO userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

            User user = new User
            {
                Username = userDto.Username,
                PasswordHash = hashedPassword,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user.UserId);
        }
    }
}