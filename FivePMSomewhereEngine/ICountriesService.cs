﻿namespace FivePMSomewhereEngine;

public interface ICountriesService
{
    IEnumerable<string> GetCountriesByTimeZone(string timeZoneName);
    string GetRandomCountryByTimeZone(string timeZoneName, string? currentCountry = null, string? selectedCountryName = null);
}
