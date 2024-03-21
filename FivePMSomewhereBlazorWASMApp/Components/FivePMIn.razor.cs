using FivePMSomewhereShared.Models;
using Microsoft.AspNetCore.Components;

namespace FivePMSomewhereBlazorWASMApp.Components
{
    public partial class FivePMIn
    {
        [Parameter]
        public TargetTimeModel? CurrentTimeZone { get; set; }

        [Parameter]
        public TimeAfterTargetModel? PreviousTimeZone { get; set; }

        private string? Country => CurrentCountry ?? PreviousCountry;

        private string? CurrentCountry => CurrentTimeZone?.RandomCountry;

        private string? PreviousCountry => PreviousTimeZone?.RandomCountry;
    }
}
