namespace Inventory.API.Models
{
    public class StorageLevel
    {
        public int StorageLevelId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? ParentId { get; set; }
        public int UserId { get; set; }


        public StorageLevel? Parent { get; set; }
        public ICollection<StorageLevel>? Children { get; set; }
        public ICollection<Item>? Items { get; set; }
        public User? User { get; set; }

        public StorageLevel()
        {
            Children = new HashSet<StorageLevel>();
            Items = new HashSet<Item>();
        }
    }
}