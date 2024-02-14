using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyIoTService.Data.Entities
{
    /// <summary>
    /// Represents an IoT device with its properties and associated data.
    /// </summary>
    public class Device : BaseEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier for the device.
        /// </summary>
        [Key]
        public int DeviceId { get; set; }

        /// <summary>
        /// Gets or sets the user ID associated with the device.
        /// </summary>
        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the name of the device.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string DeviceName { get; set; } = string.Empty;

        /// <summary>
        /// Navigation property for the data associated with the device.
        /// </summary>
        public List<DeviceData> DeviceData { get; set; } = new List<DeviceData>();
    }
}