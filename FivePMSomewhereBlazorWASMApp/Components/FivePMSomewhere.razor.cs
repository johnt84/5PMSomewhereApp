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
                CurrentTimeZone = GetCurrentTimeZone(applicableTimeZones.CurrentTimeZones);

                PreviousTimeZone = GetPreviousTimeZone(applicableTimeZones.PreviousTimeZones);

                NextTimeZone = GetNextTimeZone(applicableTimeZones.NextTimeZones);
            }
        }

        private TargetTimeModel? GetCurrentTimeZone(IEnumerable<TargetTimeModel> currentTimeZones)
        {
            if (currentTimeZones is null || !currentTimeZones.Any())
            {
                return null;
            }

            if (currentTimeZones.Count() == 1)
            {
                return currentTimeZones.Single();
            }
            else
            {
                var random = new Random();

                int randomPosition = random.Next(currentTimeZones.Count());

                return currentTimeZones.ToArray()[randomPosition];
            }
        }

        private TimeAfterTargetModel? GetPreviousTimeZone(IEnumerable<TimeAfterTargetModel> previousTimeZones)
        {
            if (previousTimeZones is null || !previousTimeZones.Any())
            {
                return null;
            }

            if (previousTimeZones.Count() == 1)
            {
                return previousTimeZones.Single();
            }
            else
            {
                var random = new Random();

                int randomPosition = random.Next(previousTimeZones.Count());

                return previousTimeZones.ToArray()[randomPosition];
            }
        }

        private TimeBeforeTargetModel? GetNextTimeZone(IEnumerable<TimeBeforeTargetModel> nextTimeZones)
        {
            if (nextTimeZones is null || !nextTimeZones.Any())
            {
                return null;
            }

            if (nextTimeZones.Count() == 1)
            {
                return nextTimeZones.Single();
            }
            else
            {
                var random = new Random();

                int randomPosition = random.Next(nextTimeZones.Count());

                return nextTimeZones.ToArray()[randomPosition];
            }
        }
    }
}
