using Inventory.API.Data;
using Inventory.API.Models;
using Inventory.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventory.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly InventoryDbContext _context;
        private readonly TokenService _tokenService;

        public UsersController(InventoryDbContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="user">User object containing Username, Password.</param>
        /// <returns>Returns status code indicating success or failure.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (await _context.Users.AnyAsync(u => u.Username == user.Username))
                return BadRequest("User already exists");

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return Ok("Registration successful");
        }

        /// <summary>
        /// Authenticates user credentials.
        /// </summary>
        /// <param name="login">User object containing login credentials.</param>
        /// <returns>Returns token if successful, unauthorized if not.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == login.Username && u.Password == login.Password);
            if (user == null)
            {
                return Unauthorized("Invalid credentials");
            }
            var token = _tokenService.GenerateJwtToken(user);
            return Ok(new { Token = token, user.Username, user.UserId, user.IsAdmin });
        }
    }
}