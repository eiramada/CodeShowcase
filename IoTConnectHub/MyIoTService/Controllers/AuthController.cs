using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyIoTService.Data.Data;
using MyIoTService.Data.Entities;
using MyIoTService.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyIoTService.Controllers
{
    /// <summary>
    /// Controller for authentication-related actions.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly MyIoTDataContext _context;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="context">The database context used for data access.</param>
        /// <param name="configuration">Configuration properties, typically read from a configuration file.</param>

        public AuthController(MyIoTDataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        /// <summary>
        /// Authenticates a user and generates a JWT token.
        /// </summary>
        /// <param name="loginDto">The login data transfer object containing user credentials.</param>
        /// <response code="200">Returns a JWT token and user ID upon successful authentication.</response>
        /// <response code="400">Returned if the login DTO is null.</response>
        /// <response code="401">Returned if the credentials are invalid.</response>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            if (loginDto == null)
            {
                return BadRequest();
            }

            bool isValidUser = await ValidateUserCredentials(loginDto.UserName, loginDto.Password);
            if (!isValidUser)
            {
                return Unauthorized("Invalid credentials");
            }

            User? user = await GetUserByUsernameAsync(loginDto.UserName);
            if (user == null) return NotFound("User not found.");

            string tokenString = GenerateJSONWebToken(user);
            return Ok(new { Token = tokenString, user.UserId });
        }

        /// <summary>
        /// Validates a user's credentials.
        /// </summary>
        /// <param name="userName">The user's username.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>A boolean indicating whether the credentials are valid.</returns>
        private async Task<bool> ValidateUserCredentials(string userName, string password)
        {
            User? user = await GetUserByUsernameAsync(userName);
            return user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        }

        /// <summary>
        /// Generates a JWT token for the specified user.
        /// </summary>
        /// <param name="user">The user for whom to generate the token.</param>
        /// <returns>A JWT token as a string.</returns>
        private string GenerateJSONWebToken(User user)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Retrieves a user by username.
        /// </summary>
        /// <param name="username">The username of the user to retrieve.</param>
        /// <returns>The user if found; otherwise, null.</returns>
        private async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
