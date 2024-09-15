using FivePMSomewhereShared.Models;

namespace FivePMSomewhereBlazorWASMApp.Logic;

public static class CountryLogic
{
    public static string? GetCountry(TargetTimeModel? CurrentTimeZone, TimeAfterTargetModel? PreviousTimeZone)
    {
        string? CurrentCountry = CurrentTimeZone?.RandomCountry;

        string? PreviousCountry = PreviousTimeZone?.RandomCountry;

        return CurrentCountry ?? PreviousCountry;
    }
}
