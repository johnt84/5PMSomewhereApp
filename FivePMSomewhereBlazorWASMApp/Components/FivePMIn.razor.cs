using FivePMSomewhereBlazorWASMApp.Logic;
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

        private string? Country => CountryLogic.GetCountry(CurrentTimeZone, PreviousTimeZone);
    }
}
