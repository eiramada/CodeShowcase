using System.ComponentModel.DataAnnotations;

namespace MyIoTService.DTO
{
    public class DeviceDto
    {
        public int DeviceId { get; set; }
        public int UserId { get; set; }

        [Required(ErrorMessage = "Device name is required.")]
        [StringLength(100, ErrorMessage = "Device name must not exceed 100 characters.")]
        public string DeviceName { get; set; } = string.Empty;
    }
}
