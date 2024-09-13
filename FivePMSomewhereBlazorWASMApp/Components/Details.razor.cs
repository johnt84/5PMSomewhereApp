using FivePMSomewhereShared.Models;
using Microsoft.AspNetCore.Components;

namespace FivePMSomewhereBlazorWASMApp.Components;

public partial class Details
{
    [Parameter]
    public TimeZoneModel? TimeZone { get; set; }
}
