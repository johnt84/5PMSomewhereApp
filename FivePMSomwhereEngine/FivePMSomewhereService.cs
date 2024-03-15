using FivePMShared.Constants;
using FivePMShared.Models;

namespace FivePMSomewhereEngine;

public class FivePMSomewhereService
{
    private readonly ICountriesService _countriesService;

    public FivePMSomewhereService(ICountriesService countriesService)
    {
        _countriesService = countriesService;
    }
           
    public FivePmModel GetApplicableTimeZones(DateTime searchDate)
    {
        var targetTime = new TimeSpan(TargetTime.TargetHour, 0, 0);
            
        var timeZones = TimeZoneInfo.GetSystemTimeZones();

        if (timeZones is null)
        {
            return null;
        }

        var targetDate = new DateTime(searchDate.Year, searchDate.Month, searchDate.Day, TargetTime.TargetHour, 0, 0);

        int numberOfHours = TargetTime.TargetHour - searchDate.Hour;

        var targetHourTimeZones = timeZones
                        .Where(timeZone => timeZone.BaseUtcOffset.Hours == numberOfHours);

        bool isTargetTime = searchDate.Hour == TargetTime.TargetHour && searchDate.Minute == 0;

        if (isTargetTime)
        {
            return new FivePmModel
            {
                CurrentTimeZones = targetHourTimeZones
                                    .Select(timeZone => new TargetTimeModel
                                    {
                                        TimeZoneName = timeZone.DisplayName,
                                        UtcOffset = timeZone.BaseUtcOffset.Hours,
                                        TimeAtOffset = searchDate.AddHours(timeZone.BaseUtcOffset.Hours),
                                        Countries = _countriesService.GetCountriesByTimeZone(timeZone.DisplayName)
                                    })
            };
        }

        var currentTimeZones = targetHourTimeZones
                                   .Where(timeZone => searchDate.AddHours(timeZone.BaseUtcOffset.Hours) == targetDate)
                                   .Select(timeZone => new TargetTimeModel
                                   {
                                       TimeZoneName = timeZone.DisplayName,
                                       UtcOffset = timeZone.BaseUtcOffset.Hours,
                                       TimeAtOffset = searchDate.AddHours(timeZone.BaseUtcOffset.Hours),
                                       Countries = _countriesService.GetCountriesByTimeZone(timeZone.DisplayName)
                                   });

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

        var previousTimeZones = targetHourTimeZones
                                    .Select(timeZone => new TimeAfterTargetModel
                                    {
                                        TimeZoneName = timeZone.DisplayName,
                                        UtcOffset = timeZone.BaseUtcOffset.Hours,
                                        TimeAtOffset = searchDate.AddHours(timeZone.BaseUtcOffset.Hours),
                                        NumberOfMinutesAfterTarget = (searchDate.AddHours(timeZone.BaseUtcOffset.Hours) - targetDate).Minutes,
                                        Countries = _countriesService.GetCountriesByTimeZone(timeZone.DisplayName)
                                    });

        var nextTimeZones = timeZones
                            .Where(timeZone => timeZone.BaseUtcOffset == firstUtcOffsetAfterTargetTime)
                            .Select(timeZone => new TimeBeforeTargetModel
                            {
                                TimeZoneName = timeZone.DisplayName,
                                UtcOffset = timeZone.BaseUtcOffset.Hours,
                                TimeAtOffset = searchDate.AddHours(timeZone.BaseUtcOffset.Hours),
                                NumberOfMinutesBeforeTarget = (targetDate - searchDate.AddHours(timeZone.BaseUtcOffset.Hours)).Minutes,
                                Countries = _countriesService.GetCountriesByTimeZone(timeZone.DisplayName)
                            });

        return new FivePmModel()
        {
            NextTimeZones = nextTimeZones,
            PreviousTimeZones = previousTimeZones
        };
    }
}