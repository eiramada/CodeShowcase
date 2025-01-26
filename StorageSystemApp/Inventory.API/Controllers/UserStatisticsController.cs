using Inventory.API.Data;
using Inventory.API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventory.API.Controllers
{
    /// <summary>
    /// Handles the requests for fetching user statistics.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UserStatisticsController : ControllerBase
    {
        private readonly InventoryDbContext _context;

        public UserStatisticsController(InventoryDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets statistics for all users including total items, storage levels, and other details.
        /// Accessible only by an admin
        /// </summary>
        /// <returns>A list of user statistics.</returns>
        [HttpGet, Authorize]
        public async Task<IActionResult> GetUserStatistics()
        {
            var isAdmin = User.Claims.FirstOrDefault(c => c.Type == "isAdmin" && c.Value == "True")?.Value == "True";
            if (!isAdmin)
            {
                return Unauthorized("This action is only available to administrators.");
            }

            var userStatistics = await _context.Users
                     .Include(u => u.StorageLevels)
                     .ThenInclude(sl => sl.Items)
                     .Select(user => new UserStatisticsDto
                     {
                         UserId = user.UserId,
                         UserJoined = user.CreatedOn, 
                         UserName = user.Username,
                         TotalItems = user.StorageLevels.SelectMany(sl => sl.Items).Count(),
                         TotalStorageLevels = user.StorageLevels.Count,
                         ItemsWithDescription = user.StorageLevels.SelectMany(sl => sl.Items).Count(item => !string.IsNullOrEmpty(item.Description)),
                         ItemsWithImage = user.StorageLevels.SelectMany(sl => sl.Items).Count(item => !string.IsNullOrEmpty(item.ImagePath)),
                     })
                     .ToListAsync();

            return Ok(userStatistics);
        }
    }
}
