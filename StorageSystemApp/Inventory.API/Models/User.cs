namespace Inventory.API.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsAdmin { get; set; } = false;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;


        public ICollection<Item>? Items { get; set; }
        public ICollection<StorageLevel>? StorageLevels { get; set; }

        public User()
        {
            Items = new HashSet<Item>();
            StorageLevels = new HashSet<StorageLevel>();
        }
    }
}
