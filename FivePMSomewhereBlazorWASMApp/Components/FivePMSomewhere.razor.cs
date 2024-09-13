using FivePMSomewhereBlazorWASMApp.Logic;
using FivePMSomewhereEngine;
using FivePMSomewhereShared.Models;
using Microsoft.AspNetCore.Components;

namespace FivePMSomewhereBlazorWASMApp.Components;

public partial class FivePMSomewhere
{
    [Inject]
    private ITimeZoneService TimeZoneService { get; set; } = null!;

    private TimeZoneModel? TimeZone { get; set; }

    private string? Country => CountryLogic.GetCountry(TimeZone?.CurrentTimeZone, TimeZone?.PreviousTimeZone);

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
        TimeZone = TimeZoneService.GetSelectedTimeZones(currentCountry: currentCountry);
    }
}
