using Inventory.API.Data;
using Inventory.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Inventory.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ItemsController : ControllerBase
    {
        private readonly InventoryDbContext _context;

        public ItemsController(InventoryDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] Item item)
        {
            await _context.Items.AddAsync(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetItem), new { id = item.ItemId }, item);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(int id)
        {

            var item = await FindItem(i => i.ItemId == id);
            if (item == null)
                return NotFound(new ErrorResponse("Item not found"));

            return Ok(item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, [FromBody] Item updatedItem)
        {
            var item = await FindItem(i => i.ItemId == id);
            if (item == null)
                return NotFound(new ErrorResponse("Item not found"));

            UpdateItemFields(item, updatedItem);
            await _context.SaveChangesAsync();
            return Ok(item);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchItems(string search)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
                return BadRequest("Invalid user ID");

            var items = await _context.Items
                                      .Include(i => i.StorageLevel)
                        .Where(i => i.StorageLevel.UserId == userId && 
                                    (EF.Functions.Like(i.Title, $"%{search}%")
                                  || EF.Functions.Like(i.SerialNumber, $"%{search}%")
                                  || EF.Functions.Like(i.Description, $"%{search}%")
                                  || EF.Functions.Like(i.Category, $"%{search}%")
                                               || EF.Functions.Like(i.StorageLevel.Name, $"%{search}%")) )
                        .ToListAsync();

            return Ok(items);
        }

        private async Task<Item> FindItem(Expression<Func<Item, bool>> predicate)
        {
            return await _context.Items.FirstOrDefaultAsync(predicate);
        }
        private void UpdateItemFields(Item existingItem, Item newItem)
        {
            existingItem.Title = newItem.Title;
            existingItem.SerialNumber = newItem.SerialNumber;
            existingItem.ImagePath = newItem.ImagePath;
            existingItem.Quantity = newItem.Quantity;
            existingItem.Description = newItem.Description;
            existingItem.Category = newItem.Category;
            existingItem.StorageLevelId = newItem.StorageLevelId;
        }
    }
}
