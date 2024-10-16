﻿using FivePMSomewhereShared.Constants;
using FivePMSomewhereShared.Models;

namespace FivePMSomewhereEngine;

public class FivePMSomewhereService : IFivePMSomewhereService
{
    private readonly ICountriesService _countriesService;

    public FivePMSomewhereService(ICountriesService countriesService)
    {
        _countriesService = countriesService;
    }
           
    public FivePmModel? GetApplicableTimeZones
    (DateTime? searchDate = null,
        string? currentCountry = null,
        string? selectedTimeZoneName = null,
        int? selectedCountryId = null)
    {
        var targetTime = new TimeSpan(TargetTime.TargetHour, 0, 0);
            
        var timeZones = TimeZoneInfo.GetSystemTimeZones();

        if (timeZones is null)
        {
            return null;
        }

        DateTime date;

        if (searchDate is not null)
        {
            date = searchDate.Value;
        }
        else
        {
            date = DateTime.UtcNow;
        }

        var targetDate = new DateTime(date.Year, date.Month, date.Day, TargetTime.TargetHour, 0, 0);

        int numberOfHours = TargetTime.TargetHour - date.Hour;

        var targetHourTimeZones = timeZones
                        .Where(timeZone => timeZone.BaseUtcOffset.Hours == numberOfHours);

        bool isTargetTime = date.Hour == TargetTime.TargetHour && date.Minute == 0;

        if (isTargetTime)
        {
            return new FivePmModel
            {
                CurrentTimeZones = targetHourTimeZones
                                    .Select(timeZone => new TargetTimeModel
                                    {
                                        TimeZoneName = timeZone.DisplayName,
                                        UtcOffset = timeZone.BaseUtcOffset.Hours,
                                        TimeAtOffset = date.AddHours(timeZone.BaseUtcOffset.Hours),
                                        Countries = _countriesService.GetCountriesByTimeZone(timeZone.DisplayName),
                                        RandomCountry = _countriesService.GetRandomCountryByTimeZone(timeZone.DisplayName, currentCountry: currentCountry
                                                            , selectedTimeZoneName: selectedTimeZoneName
                                                            , selectedCountryId: selectedCountryId)
                                    })
            };
        }

        var allCurrentTimeZones = targetHourTimeZones
                                   .Where(timeZone => date.AddHours(timeZone.BaseUtcOffset.Hours) == targetDate)
                                   .Select(timeZone => new TargetTimeModel
                                   {
                                       TimeZoneName = timeZone.DisplayName,
                                       UtcOffset = timeZone.BaseUtcOffset.Hours,
                                       TimeAtOffset = date.AddHours(timeZone.BaseUtcOffset.Hours),
                                       Countries = _countriesService.GetCountriesByTimeZone(timeZone.DisplayName),
                                       RandomCountry = _countriesService.GetRandomCountryByTimeZone(timeZone.DisplayName, currentCountry: currentCountry
                                                            , selectedTimeZoneName: selectedTimeZoneName
                                                            , selectedCountryId: selectedCountryId)
                                   });

        var currentTimeZoneWithSelection = allCurrentTimeZones
                                                .Where(timeZone => selectedTimeZoneName is null || timeZone.TimeZoneName == selectedTimeZoneName);

        var currentTimeZones = currentTimeZoneWithSelection.Any() ? currentTimeZoneWithSelection : allCurrentTimeZones;

        if (currentTimeZones.Any())
        {
            return new FivePmModel
            {
                CurrentTimeZones = currentTimeZones
            };
        }

        var firstUtcOffsetAfterTargetTime = timeZones
                                        .Where(timeZone => timeZone.BaseUtcOffset.Hours < numberOfHours)
                                        .GroupBy(timeZone => timeZone.BaseUtcOffset)
                                        .OrderByDescending(TimeZone => TimeZone.Key)
                                        .Select(TimeZone => TimeZone.Key)
                                        .First();

        var allPreviousTimeZones = targetHourTimeZones
                                    .Select(timeZone => new TimeAfterTargetModel
                                    {
                                        TimeZoneName = timeZone.DisplayName,
                                        UtcOffset = timeZone.BaseUtcOffset.Hours,
                                        TimeAtOffset = date.AddHours(timeZone.BaseUtcOffset.Hours),
                                        NumberOfMinutesAfterTarget = (date.AddHours(timeZone.BaseUtcOffset.Hours) - targetDate).Minutes,
                                        Countries = _countriesService.GetCountriesByTimeZone(timeZone.DisplayName),
                                        RandomCountry = _countriesService.GetRandomCountryByTimeZone(timeZone.DisplayName, currentCountry: currentCountry
                                                            , selectedTimeZoneName: selectedTimeZoneName
                                                            , selectedCountryId: selectedCountryId)
                                    });

        var previousTimeZoneWithSelection = allPreviousTimeZones
                                                .Where(timeZone => selectedTimeZoneName is null || timeZone.TimeZoneName == selectedTimeZoneName);

        var previousTimeZones = previousTimeZoneWithSelection.Any() ? previousTimeZoneWithSelection : allPreviousTimeZones;

        var nextTimeZones = timeZones
                            .Where(timeZone => timeZone.BaseUtcOffset == firstUtcOffsetAfterTargetTime)
                            .Select(timeZone => new TimeBeforeTargetModel
                            {
                                TimeZoneName = timeZone.DisplayName,
                                UtcOffset = timeZone.BaseUtcOffset.Hours,
                                TimeAtOffset = date.AddHours(timeZone.BaseUtcOffset.Hours),
                                NumberOfMinutesBeforeTarget = (targetDate - date.AddHours(timeZone.BaseUtcOffset.Hours)).Minutes,
                                Countries = _countriesService.GetCountriesByTimeZone(timeZone.DisplayName),
                                RandomCountry = _countriesService.GetRandomCountryByTimeZone(timeZone.DisplayName, currentCountry: currentCountry)
                            });

        return new FivePmModel()
        {
            NextTimeZones = nextTimeZones,
            PreviousTimeZones = previousTimeZones
        };
    }
}