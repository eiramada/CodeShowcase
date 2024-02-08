using Dapper;
using Dapper.Contrib.Extensions;
using FormEnhancer.Data;
using FormEnhancer.Data.Models;
using System.Data;

namespace FormEnhancer.Repository
{
    public class SectorRepository : ISectorRepository
    {
        private readonly IDataAccess _db;
        public SectorRepository(IDataAccess db)
        {
            _db = db;
        }

        public async Task<IEnumerable<SectorEntity>> GetSectorsTreeAsync()
        {
            string sql = "SELECT * FROM dbo.Sectors";

            using IDbConnection connection = _db.Create();

            IEnumerable<SectorEntity> allSectors = await connection.QueryAsync<SectorEntity>(sql);

            await GetSectorDistance(allSectors);

            await GetSubSectors(allSectors.ToList(), null);

            allSectors = allSectors.Where(x => x.ParentId == null).ToList();

            return allSectors;
        }

        public async Task InsertSectorsAsync(IEnumerable<SectorEntity> sectors)
        {
            IEnumerable<SectorEntity> sectorsFromDb = await GetSectorsTreeAsync();
            if (sectorsFromDb.Any())
            {
                return;
            }

            using IDbConnection connection = _db.Create();

            connection.Open();

            using IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                await connection.InsertAsync(sectors, transaction);

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
            }
        }

        private async Task<IList<SectorEntity>> GetSubSectors(IList<SectorEntity> allSectors, SectorEntity? parentSector)
        {
            IEnumerable<SectorEntity> sectors = allSectors.Where(x => x.ParentId == parentSector?.Id);

            foreach (SectorEntity? sector in sectors)
            {
                sector.SubSectors = await GetSubSectors(allSectors, sector);
            }

            return sectors.ToList();
        }

        private async Task GetSectorDistance(IEnumerable<SectorEntity> allSectors)
        {
            foreach (SectorEntity sector in allSectors)
            {
                string sql = @"WITH cte(ParentID) AS(
                            SELECT ParentID FROM Sectors WHERE Id = @id
                            UNION ALL
                            SELECT i.ParentID FROM cte c
                            INNER JOIN Sectors i ON c.ParentID = i.Id)

                        SELECT COUNT(ParentID)
                        FROM cte
                        WHERE ParentID IS NOT NULL";

                using IDbConnection connection = _db.Create();

                int distance = await connection.QuerySingleAsync<int>(sql, new { sector.Id });
                sector.Distance = distance;
            }
        }
    }
}
