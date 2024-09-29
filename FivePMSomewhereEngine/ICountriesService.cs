namespace FivePMSomewhereEngine;

public interface ICountriesService
{
    IEnumerable<string> GetCountriesByTimeZone(string timeZoneName, int? selectedCountryId = null);
    string GetRandomCountryByTimeZone(string timeZoneName, string? currentCountry = null, string? selectedTimeZoneName = null, int? selectedCountryId = null);
    int? GetCountryId(string? countryName);
}
