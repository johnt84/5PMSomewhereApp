using FivePMSomewhereShared.Constants;
using RESTCountries.NET.Models;
using RESTCountries.NET.Services;

namespace FivePMSomewhereEngine;

public class CountriesService : ICountriesService
{
    private readonly IEnumerable<Country> _countries;

    public CountriesService()
    {
        _countries = RestCountriesService.GetAllCountries();
    }

    public IEnumerable<string> GetCountriesByTimeZone(string timeZoneName, int? selectedCountryId = null)
    {
        string timeZoneNameForCountrySearch = GetTimeZoneForCountrySearch(timeZoneName);

        string? selectedCountryName = null;

        if (selectedCountryId is not null && selectedCountryId < _countries.Count())
        {
            selectedCountryName = _countries
                                    .OrderBy(country => country.Name.Common)
                                    .ToArray()[selectedCountryId.Value].Name.Common;
        }

        var timeZoneCountries = _countries
                        .Where(country => country.Timezones.ToList().Contains(timeZoneNameForCountrySearch))
                        .Select(country => country.Name.Common);

        if (selectedCountryName is null || !timeZoneCountries.Contains(selectedCountryName))
        {
            return timeZoneCountries;
        }

        return new[] { selectedCountryName };
    }

    public string GetRandomCountryByTimeZone(string timeZoneName, string? currentCountry = null
                    , string? selectedTimeZoneName = null, int? selectedCountryId = null)
    {
        var countries = GetCountriesByTimeZone(timeZoneName, selectedCountryId: selectedCountryId);

        if (!countries.Any())
        {
            return string.Empty;
        }

        if (selectedTimeZoneName is not null 
            && selectedCountryId is not null 
            && selectedTimeZoneName == timeZoneName)
        {
            return countries.First();
        }

        string? selectableCountry = GetSelectableCountry(countries, timeZoneName);

        bool allowSelectableCountry = selectableCountry is not null && 
                        (Countries.Values.Contains(selectableCountry) || selectableCountry != currentCountry);

        if (allowSelectableCountry)
        {
            return selectableCountry!;
        }

        string randomCountry = string.Empty;

       while (randomCountry == string.Empty || 
            (randomCountry == currentCountry && countries.Count() > 1))
       {
            var rand = new Random();
            int countryIndex = rand.Next(countries.Count());

            randomCountry = countries.ToArray()[countryIndex];
        }

        return randomCountry;
    }

    public int? GetCountryId(string? countryName)
    {
        if (countryName is null)
        {
            return null;
        }

        return _countries
                .OrderBy(country => country.Name.Common)
                .ToList()
                .FindIndex(country => country.Name.Common == countryName);
    }

    private string GetTimeZoneForCountrySearch(string timeZoneName)
    {
        var timeZoneNameSplit = timeZoneName.Split(")").ToList();

        var timeZone = timeZoneNameSplit[0].Replace("(", string.Empty);

        return timeZone.Replace("+00:00", string.Empty);
    }
    
    private string? GetSelectableCountry(IEnumerable<string> countries, string timeZoneName)
    {
        string? selectableCountry = null;
        
        foreach(var country in countries)
        {
            if (Countries.Values.Contains(country) && TimeZoneNames.Values.Contains(timeZoneName))
            {
                if (timeZoneName == TimeZoneNames.USTimeZoneName && country != Countries.USA)
                {
                    continue;
                }

                selectableCountry = country;

                break;
            }
        }

        return selectableCountry;
    }
}
