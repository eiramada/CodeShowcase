using FormEnhancer.Data.Models;

namespace FormEnhancer.Services
{
    public interface ISectorService
    {
        Task InsertSectorsToDbAsync();
        Task<IEnumerable<SectorEntity>> GetSectorsAsync();
        string CreateTreeMarkup(SectorEntity sector);
    }
}