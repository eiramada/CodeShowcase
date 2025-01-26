namespace Inventory.API.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public int Quantity { get; set; } = 1;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int StorageLevelId { get; set; }
        public StorageLevel? StorageLevel { get; set; }
    }
}
