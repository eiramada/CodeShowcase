using FormEnhancer.Data.Models;

namespace FormEnhancer.Repository
{
    public interface IReplyRepository
    {
        Task<ReplyEntity?> GetReplyAsync(int id);
        Task<IEnumerable<ReplyEntity>> GetRepliesAsync();
        Task<bool> InsertOrUpdateReplyAsync(ReplyEntity reply);
    }
}