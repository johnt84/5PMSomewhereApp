﻿using FivePMSomewhereShared.Constants;
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

    public IEnumerable<string> GetCountriesByTimeZone(string timeZoneName)
    {
        string timeZoneNameForCountrySearch = GetTimeZoneForCountrySearch(timeZoneName);

        return _countries
                .Where(country => country.Timezones.ToList().Contains(timeZoneNameForCountrySearch))
                .Select(country => country.Name.Common);
    }

    public string GetRandomCountryByTimeZone(string timeZoneName, string? currentCountry = null)
    {
        var countries = GetCountriesByTimeZone(timeZoneName);

        if (!countries.Any())
        {
            return string.Empty;
        }

        string? selectableCountry = GetSelectableCountry(countries, timeZoneName);

        if (!string.IsNullOrEmpty(selectableCountry) && selectableCountry != currentCountry)
        {
            return selectableCountry;
        }

        string randomCountry = string.Empty;

       while (randomCountry == string.Empty || (randomCountry == currentCountry))
        {
            var rand = new Random();
            int countryIndex = rand.Next(countries.Count());

            randomCountry = countries.ToArray()[countryIndex];
        }

        return randomCountry;
    }

    private string GetTimeZoneForCountrySearch(string timeZoneName)
    {
        var timeZoneNameSplit = timeZoneName.Split(")").ToList();

        return timeZoneNameSplit[0].Replace("(", string.Empty);
    }
    
    private string? GetSelectableCountry(IEnumerable<string> countries, string timeZoneName)
    {
        string? selectableCountry = null;
        
        foreach(var country in countries)
        {
            if (Countries.Values.Contains(country) && TimeZoneNames.Values.Contains(timeZoneName))
            {
                selectableCountry = country;

                break;
            }
        }

        return selectableCountry;
    }
}
