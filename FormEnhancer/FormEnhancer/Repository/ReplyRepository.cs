using Dapper;
using Dapper.Contrib.Extensions;
using FormEnhancer.Data;
using FormEnhancer.Data.Models;
using System.Data;

namespace FormEnhancer.Repository
{
    public class ReplyRepository : IReplyRepository
    {
        private readonly IDataAccess _db;

        public ReplyRepository(IDataAccess db)
        {
            _db = db;
        }

        public async Task<ReplyEntity?> GetReplyAsync(int id)
        {
            string sql = "SELECT * FROM dbo.Replies WHERE Id = @Id";

            using IDbConnection connection = _db.Create();

            var result = await connection.QuerySingleOrDefaultAsync<ReplyEntity>(sql, new { Id = id });

            return result;
        }

        public async Task<IEnumerable<ReplyEntity>> GetRepliesAsync()
        {
            string sql = "SELECT * FROM dbo.Replies";

            using IDbConnection connection = _db.Create();

            IEnumerable<ReplyEntity> result = await connection.QueryAsync<ReplyEntity>(sql);

            return result;
        }

        public async Task<bool> InsertOrUpdateReplyAsync(ReplyEntity reply)
        {
            using IDbConnection connection = _db.Create();
            bool isSaved;
            connection.Open();

            using IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                ReplyEntity? previousReply = await GetReplyAsync(reply.Id);
                if (previousReply != null)
                {
                    await connection.UpdateAsync(reply, transaction);
                }
                else
                {
                    await connection.InsertAsync(reply, transaction);
                }

                transaction.Commit();
                isSaved = true;
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
            return isSaved;
        }
    }
}
