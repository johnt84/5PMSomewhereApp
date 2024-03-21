using FivePMSomewhereShared.Models;
using Microsoft.AspNetCore.Components;

namespace FivePMSomewhereBlazorWASMApp.Components
{
    public partial class Details
    {
        [Parameter]
        public TargetTimeModel? CurrentTimeZone { get; set; }

        [Parameter]
        public TimeAfterTargetModel? PreviousTimeZone { get; set; }

        [Parameter]
        public TimeBeforeTargetModel? NextTimeZone { get; set; }
    }
}
