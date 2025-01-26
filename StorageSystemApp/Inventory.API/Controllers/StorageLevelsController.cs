using Inventory.API.Data;
using Inventory.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Inventory.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class StorageLevelsController : ControllerBase
    {
        private readonly InventoryDbContext _context;

        public StorageLevelsController(InventoryDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        /// <summary>
        /// Retrieves a storage level by ID, optionally includes items.
        /// </summary>
        /// <param name="id">The ID of the storage level to retrieve.</param>
        /// <param name="includeItems">Flag to include items in the response.</param>
        /// <returns>Returns the requested storage level if found, optionally including items.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLevel(int id, bool includeItems = false)
        {
            var query = _context.StorageLevels.AsQueryable();

            if (includeItems)
            {
                query = query.Include(l => l.Items);
            }

            var level = await query.FirstOrDefaultAsync(l => l.StorageLevelId == id);
            if (level == null)
                return NotFound(new ErrorResponse("Storage level not found"));

            return Ok(level);
        }

        /// <summary>
        /// Creates a new storage level.
        /// </summary>
        /// <param name="level">Storage level details.</param>
        /// <returns>Returns created storage level information.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateStorageLevel([FromBody] StorageLevel level)
        {
            if (level == null || string.IsNullOrWhiteSpace(level.Name))
                return BadRequest("Invalid storage level data");

            var userId = GetCurrentUserId();
            if (userId == null)
                return BadRequest("Invalid user data");

            level.UserId = (int)userId;

            await _context.StorageLevels.AddAsync(level);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetLevel), new { id = level.StorageLevelId }, level);
        }

        /// <summary>
        /// Retrieves all storage levels for the authenticated user.
        /// </summary>
        /// <returns>A list of storage levels without related entities.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllStorageLevelsForUser()
        {
            int? userId = GetCurrentUserId();
            if (!userId.HasValue)
                return BadRequest("Invalid user ID.");

            var levels = await _context.StorageLevels
                .Where(s => s.UserId == userId)
                .Select(l => new { l.StorageLevelId, l.Name, l.ParentId, l.Items })
                .ToListAsync();
            return Ok(levels);
        }

        private async Task<List<StorageLevel>> GetDescendantsAsync(int parentId)
        {
            var results = new List<StorageLevel>();
            var children = await _context.StorageLevels.Where(l => l.ParentId == parentId).ToListAsync();
            foreach (var child in children)
            {
                results.Add(child);
                results.AddRange(await GetDescendantsAsync(child.StorageLevelId));
            }
            return results;
        }

        private int? GetCurrentUserId()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            if (int.TryParse(userIdClaim, out int userId))
                return userId;

            return null;
        }
    }
}
