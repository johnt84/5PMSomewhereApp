using FivePMSomewhereShared.Models;

namespace FivePMSomewhereEngine;

public interface ITimeZoneService
{
    TimeZoneModel? GetSelectedTimeZones(DateTime? searchDate = null, string? currentCountry = null);
}
