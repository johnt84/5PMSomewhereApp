using FivePMSomewhereBlazorWASMApp.Logic;
using FivePMSomewhereEngine;
using FivePMSomewhereShared.Models;
using Microsoft.AspNetCore.Components;

namespace FivePMSomewhereBlazorWASMApp.Components
{
    public partial class FivePMSomewhere
    {
        [Inject]
        private IFivePMSomewhereService FivePMSomewhereService { get; set; } = null!;

        private TargetTimeModel? CurrentTimeZone { get; set; }
        private TimeAfterTargetModel? PreviousTimeZone { get; set; }
        private TimeBeforeTargetModel? NextTimeZone { get; set; }
        private string? Country => CountryLogic.GetCountry(CurrentTimeZone, PreviousTimeZone);

        protected override void OnInitialized()
        {
            LoadTimeZones();
        }

        private void btnRefreshClick()
        {
            LoadTimeZones(currentCountry: Country);
        }

        private void LoadTimeZones(string? currentCountry = null)
        {
            var applicableTimeZones = FivePMSomewhereService.GetApplicableTimeZones(currentCountry: currentCountry);

            if (applicableTimeZones is not null)
            {
                if (applicableTimeZones.CurrentTimeZones is not null && applicableTimeZones.CurrentTimeZones.Any())
                {
                    CurrentTimeZone = applicableTimeZones.CurrentTimeZones.ToList().First();
                }
                else
                {
                    if (applicableTimeZones.PreviousTimeZones is not null && applicableTimeZones.PreviousTimeZones.Any())
                    {
                        PreviousTimeZone = applicableTimeZones.PreviousTimeZones.ToList().First();
                    }

                    if (applicableTimeZones.NextTimeZones is not null && applicableTimeZones.NextTimeZones.Any())
                    {
                        NextTimeZone = applicableTimeZones.NextTimeZones.ToList().First();
                    }
                }
            }
        }
    }
}
