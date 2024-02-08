using FormEnhancer.Data.Models;
using Microsoft.AspNetCore.Components;

namespace FormEnhancer.Shared
{
    public partial class Table
    {
        [Parameter]
        public IEnumerable<ReplyEntity> Replies { get; set; } = new List<ReplyEntity>();
        [Parameter]
        public Action<int>? OnEditing { get; set; }

        private void EditRow(int replyId)
        {
            OnEditing?.Invoke(replyId);
        }
    }
}
