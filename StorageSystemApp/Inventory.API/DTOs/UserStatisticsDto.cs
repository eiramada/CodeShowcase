namespace Inventory.API.DTOs
{
    public class UserStatisticsDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int TotalItems { get; set; }
        public int TotalStorageLevels { get; set; }
        public int ItemsWithDescription { get; set; }
        public int ItemsWithImage { get; set; }
        public DateTime UserJoined { get; set; }
    }

}
