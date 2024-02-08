using FormEnhancer.Data.Models;

namespace FormEnhancer.Services
{
    public interface IReplyService
    {
        Task<ReplyEntity?> GetReplyAsync(int id);
        Task<IEnumerable<ReplyEntity>> GetRepliesAsync();
        Task<bool> SaveReplyAsync(ReplyEntity reply);
    }
}