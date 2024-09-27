using Append.Blazor.WebShare;
using FivePMSomewhereBlazorApp.Logic;
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

    private string? Country => CountryLogic.GetCountry(CurrentTimeZone, PreviousTimeZone);

    private async Task ShareAsync()
    {
        try
        {
            string title = $"It's currently 5 PM in {Country}";

            string customText = title;

            await WebShareService.ShareAsync(title, customText, NavigationManager.Uri);
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
