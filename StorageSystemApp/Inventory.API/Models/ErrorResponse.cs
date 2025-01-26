namespace Inventory.API.Models
{
    public class ErrorResponse(string message)
    {
        public string Message { get; set; } = message;
    }
}
