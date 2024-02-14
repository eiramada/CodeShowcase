using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyIoTService.Data.Entities
{
    /// <summary>
    /// Represents the data collected from an IoT device.
    /// </summary>
    public class DeviceData : BaseEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier for the device data.
        /// </summary>
        [Key]
        public int DataId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the device this data is associated with.
        /// </summary>
        [ForeignKey("Device")]
        public int DeviceId { get; set; }

        /// <summary>
        /// Gets or sets the value of the device data.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Gets or sets the timestamp when the data was recorded.
        /// </summary>
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
    }
}
