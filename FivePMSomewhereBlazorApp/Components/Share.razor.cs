using Append.Blazor.WebShare;
using FivePMSomewhereEngine;
using FivePMSomewhereShared.Models;
using Microsoft.AspNetCore.Components;

namespace FivePMSomewhereBlazorApp.Components;

public partial class Share
{
    [Parameter]
    public TargetTimeModel? CurrentTimeZone { get; set; }

    [Parameter]
    public TimeAfterTargetModel? PreviousTimeZone { get; set; }

    [Inject]
    public ITimeZoneService TimeZoneService { get; set; } = null!;

    [Inject]
    public ICountriesService CountriesService { get; set; } = null!;

    [Inject]
    public IWebShareService WebShareService { get; set; } = null!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    private string? result;

    private async Task ShareAsync()
    {
        try
        {
            var timeZone = CurrentTimeZone ?? PreviousTimeZone;

            string title = $"It's currently 5 PM in {timeZone?.RandomCountry}";
            string customText = title;

            int? timeZoneId = TimeZoneService.GetTimeZoneId(timeZone?.TimeZoneName);

            if (timeZone is null)
            {
                return;
            }

            int? countryId = CountriesService.GetCountryId(timeZone?.RandomCountry);

            if (timeZone is null)
            {
                return;
            }

            string urlSuffix = $"{timeZoneId}/{countryId}";

            string url = $"{NavigationManager.BaseUri}{urlSuffix}";

            await WebShareService.ShareAsync(title, customText, url);
        }
        catch (WebShareAbortException ex)
        {
            result = ex.Message;
        }
        catch (WebShareException ex)
        {
            result = ex.Message;
        }
        catch (Exception ex)
        {
            result = ex.Message;
        }
    }
}
