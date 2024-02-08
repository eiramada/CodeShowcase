using FormEnhancer.Data.Models;
using FormEnhancer.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace FormEnhancer.Shared
{
    public partial class Form
    {
        [CascadingParameter] 
        public ReplyEntity Reply { get; set; } = new ReplyEntity();
        [Parameter]
        public Action<bool, ReplyEntity> OnSaving { get; set; } = default!;
        [Parameter]
        public MarkupString Options { get; set; } = default!;
        [Inject]
        private IReplyService ReplyService { get; set; } = default!;

        private async Task SaveReply(EditContext editContext)
        {
            if (Reply == null) return;

            Reply.SectorIds = string.Join(",", Reply.SelectedSectorIds);

            if (!editContext.Validate())
                return;

            OnSaving?.Invoke(await ReplyService.SaveReplyAsync(Reply), Reply);
        }

        private void SelectedSectorIdsChanged(ChangeEventArgs e)
        {
            if (e.Value is not null)
            {
                Reply.SelectedSectorIds = (int[])e.Value;
            }
        }

        private void CreateNew()
        {
            Reply = new ReplyEntity();
        }
    }
}
