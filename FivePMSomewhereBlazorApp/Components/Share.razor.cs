using Append.Blazor.WebShare;
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
    public IWebShareService WebShareService { get; set; } = default!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    private string? result;

    private async Task ShareAsync()
    {
        try
        {
            var timeZone = CurrentTimeZone ?? PreviousTimeZone;

            string title = $"It's currently 5 PM in {timeZone?.RandomCountry}";
            string customText = title;

            string urlSuffix = $"{timeZone?.TimeZoneName}/{timeZone?.RandomCountry}";

            string url = $"{NavigationManager.Uri}{urlSuffix}";

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
