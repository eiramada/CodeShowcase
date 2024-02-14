namespace MyIoTService.Data.Entities
{
    /// <summary>
    /// Base class for entities, providing common properties.
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Gets or sets the ID of the user who created this entity.
        /// </summary>
        public int CreatedBy { get; set; } = 0;

        /// <summary>
        /// Gets or sets the ID of the user who last modified this entity.
        /// </summary>
        public int ModifiedBy { get; set; } = 0;
    }
}
