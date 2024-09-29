using FivePMSomewhereShared.Models;

namespace FivePMSomewhereEngine;

public interface ITimeZoneService
{
    TimeZoneModel? GetSelectedTimeZones(DateTime? searchDate = null, string? currentCountry = null, int? selectedTimeZoneId = null
        , int? selectedCountryId = null);
    int? GetTimeZoneId(string? timeZoneName);
}
