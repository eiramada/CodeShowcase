using FormEnhancer.Data.Models;
using FormEnhancer.Services;
using Microsoft.AspNetCore.Components;

namespace FormEnhancer.Pages
{
    public partial class Index
    {
        public bool IsSaved { get; set; }
        public ReplyEntity? Reply { get; set; } = new ReplyEntity();
        [Inject]
        private ISectorService SectorService { get; set; } = default!;
        [Inject]
        private IReplyService ReplyService { get; set; } = default!;
        private IEnumerable<SectorEntity> Sectors { get; set; } = new List<SectorEntity>();
        private IEnumerable<ReplyEntity> Replies { get; set; } = new List<ReplyEntity>();

        private MarkupString Options { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await SectorService.InsertSectorsToDbAsync();
            Replies = await ReplyService.GetRepliesAsync();
            Sectors = await SectorService.GetSectorsAsync();

            CreateSectorOptions(Sectors);

            await base.OnInitializedAsync();
        }

        private async void SaveReply(bool save, ReplyEntity reply)
        {
            IsSaved = save;
            Replies = await ReplyService.GetRepliesAsync();
            Reply = reply;
            StateHasChanged();
        }

        private async void EditReply(int id)
        {
            Reply = await ReplyService.GetReplyAsync(id);
            if (Reply != null)
            {
                Reply.SelectedSectorIds = string.IsNullOrWhiteSpace(Reply.SectorIds)
                    ? Array.Empty<int>()
                    : Reply.SectorIds
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(item => int.Parse(item))
                    .ToArray();
            }

            IsSaved = false;

            StateHasChanged();
        }

        private void CreateSectorOptions(IEnumerable<SectorEntity> sectors)
        {
            SectorEntity rootSector = new();

            foreach (var parentSector in sectors.Where(s => s.ParentId == null))
            {
                rootSector.SubSectors.Add(parentSector);
            }

            Options = (MarkupString)SectorService.CreateTreeMarkup(rootSector);
        }
    }
}
