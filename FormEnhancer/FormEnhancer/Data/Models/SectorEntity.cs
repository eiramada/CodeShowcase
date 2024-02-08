using Dapper.Contrib.Extensions;

namespace FormEnhancer.Data.Models
{
    [Table("Sectors")]
    public class SectorEntity
    {
        [ExplicitKey]
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int? ParentId { get; set; }
        
        [Write(false)]
        public int Distance { get; set; }

        [Write(false)]
        public IList<SectorEntity> SubSectors { get; set; } = new List<SectorEntity>();
    }
}
