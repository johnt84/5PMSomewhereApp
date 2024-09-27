using Microsoft.AspNetCore.Components;

namespace FivePMSomewhereBlazorApp.Components.Pages;

public partial class Home
{
    [Parameter]
    public string? SelectedTimeZoneName { get; set; }

    [Parameter]
    public string? SelectedCountryName { get; set; }
}
