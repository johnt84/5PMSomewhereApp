using Microsoft.AspNetCore.Components;

namespace FivePMSomewhereBlazorApp.Components.Pages;

public partial class Home
{
    [Parameter]
    public int? SelectedTimeZoneId { get; set; }

    [Parameter]
    public int? SelectedCountryId { get; set; }
}
