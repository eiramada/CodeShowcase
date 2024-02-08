using FormEnhancer.Data.Models;
using FormEnhancer.Repository;

namespace FormEnhancer.Services
{
    public class ReplyService : IReplyService
    {
        private readonly IReplyRepository _replyRepository;

        public ReplyService(IReplyRepository replyRepository)
        {
            _replyRepository = replyRepository;
        }

        public async Task<ReplyEntity?> GetReplyAsync(int id)
        {
            return await _replyRepository.GetReplyAsync(id);
        }

        public async Task<IEnumerable<ReplyEntity>> GetRepliesAsync()
        {
            return await _replyRepository.GetRepliesAsync();
        }

        public async Task<bool> SaveReplyAsync(ReplyEntity reply)
        {
            return await _replyRepository.InsertOrUpdateReplyAsync(reply);
        }
    }
}
