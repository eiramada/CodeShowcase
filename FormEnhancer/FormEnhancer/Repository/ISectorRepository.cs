using FormEnhancer.Data.Models;

namespace FormEnhancer.Repository
{
    public interface ISectorRepository
    {
        Task<IEnumerable<SectorEntity>> GetSectorsTreeAsync();
        Task InsertSectorsAsync(IEnumerable<SectorEntity> sectors);
    }
}