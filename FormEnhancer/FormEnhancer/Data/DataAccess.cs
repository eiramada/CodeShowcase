using System.Data;
using System.Data.SqlClient;

namespace FormEnhancer.Data
{
    public class DataAccess : IDataAccess
    {
        private readonly IConfiguration _config;

        public DataAccess(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection Create()
        {
            return new SqlConnection(_config.GetConnectionString("default"));
        }
    }
}
