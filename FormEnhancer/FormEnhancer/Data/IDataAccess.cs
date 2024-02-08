using System.Data;

namespace FormEnhancer.Data
{
    public interface IDataAccess
    {
        IDbConnection Create();
    }
}