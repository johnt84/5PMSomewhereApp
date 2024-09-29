using FivePMSomewhereBlazorApp.Components.TimeZone;
using FivePMSomewhereBlazorApp.Logic;
using FivePMSomewhereEngine;
using FivePMSomewhereShared.Models;
using Microsoft.AspNetCore.Components;

namespace FivePMSomewhereBlazorApp.Components;

public partial class FivePMSomewhere
{
    [Parameter]
    public int? SelectedTimeZoneId { get; set; }

    [Parameter]
    public int? SelectedCountryId { get; set; }

    [Inject]
    private ITimeZoneService TimeZoneService { get; set; } = null!;

    [Inject]
    public TimeProvider TimeProvider { get; set; } = default!;

    private TimeZoneModel? TimeZone { get; set; }

    private string? Country => CountryLogic.GetCountry(TimeZone?.CurrentTimeZone, TimeZone?.PreviousTimeZone);

    protected override void OnInitialized() =>
        LoadTimeZones();

    private void btnRefreshClick() =>
        LoadTimeZones(isRefresh: true, currentCountry: Country);

    private void LoadTimeZones(bool isRefresh = false, string? currentCountry = null)
    {
        if (isRefresh)
        {
            SelectedTimeZoneId = null;
            SelectedCountryId = null;
        }

        TimeZone = TimeZoneService.GetSelectedTimeZones(searchDate: TimeProvider.ToLocalDateTime(DateTime.UtcNow), currentCountry: currentCountry
                    , selectedTimeZoneId: SelectedTimeZoneId
                    , selectedCountryId: SelectedCountryId);
    }
}
