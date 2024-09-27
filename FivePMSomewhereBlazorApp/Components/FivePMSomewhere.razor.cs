﻿using FivePMSomewhereBlazorApp.Components.TimeZone;
using FivePMSomewhereBlazorApp.Logic;
using FivePMSomewhereEngine;
using FivePMSomewhereShared.Models;
using Microsoft.AspNetCore.Components;

namespace FivePMSomewhereBlazorApp.Components;

public partial class FivePMSomewhere
{
    [Parameter]
    public string? SelectedTimeZoneName { get; set; }

    [Parameter]
    public string? SelectedCountryName { get; set; }

    [Inject]
    private ITimeZoneService TimeZoneService { get; set; } = null!;

    [Inject]
    public TimeProvider TimeProvider { get; set; } = default!;

    private TimeZoneModel? TimeZone { get; set; }

    private string? Country => CountryLogic.GetCountry(TimeZone?.CurrentTimeZone, TimeZone?.PreviousTimeZone);

    protected override void OnInitialized() =>
        LoadTimeZones();

    private void btnRefreshClick() =>
        LoadTimeZones(currentCountry: Country);

    private void LoadTimeZones(string? currentCountry = null) =>
         TimeZone = TimeZoneService.GetSelectedTimeZones(searchDate: TimeProvider.ToLocalDateTime(DateTime.UtcNow), currentCountry: currentCountry
        , selectedTimeZoneName: SelectedTimeZoneName
        , selectedCountryName: SelectedCountryName);
}
