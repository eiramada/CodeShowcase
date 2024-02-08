using Microsoft.AspNetCore.Components;

namespace FormEnhancer.Shared
{
    public partial class SuccessAlert
    {
        [CascadingParameter]
        public bool ShowAlert { get; set; }

        private string Display => ShowAlert ? "" : "d-none";
    }
}
