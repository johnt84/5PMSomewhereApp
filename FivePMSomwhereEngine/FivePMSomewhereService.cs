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
           
    public FivePmModel GetApplicableTimeZones(DateTime? searchDate = null)
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
                                        Countries = _countriesService.GetCountriesByTimeZone(timeZone.DisplayName)
                                    })
            };
        }

        var currentTimeZones = targetHourTimeZones
                                   .Where(timeZone => date.AddHours(timeZone.BaseUtcOffset.Hours) == targetDate)
                                   .Select(timeZone => new TargetTimeModel
                                   {
                                       TimeZoneName = timeZone.DisplayName,
                                       UtcOffset = timeZone.BaseUtcOffset.Hours,
                                       TimeAtOffset = date.AddHours(timeZone.BaseUtcOffset.Hours),
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
                                        TimeAtOffset = date.AddHours(timeZone.BaseUtcOffset.Hours),
                                        NumberOfMinutesAfterTarget = (date.AddHours(timeZone.BaseUtcOffset.Hours) - targetDate).Minutes,
                                        Countries = _countriesService.GetCountriesByTimeZone(timeZone.DisplayName)
                                    });

        var nextTimeZones = timeZones
                            .Where(timeZone => timeZone.BaseUtcOffset == firstUtcOffsetAfterTargetTime)
                            .Select(timeZone => new TimeBeforeTargetModel
                            {
                                TimeZoneName = timeZone.DisplayName,
                                UtcOffset = timeZone.BaseUtcOffset.Hours,
                                TimeAtOffset = date.AddHours(timeZone.BaseUtcOffset.Hours),
                                NumberOfMinutesBeforeTarget = (targetDate - date.AddHours(timeZone.BaseUtcOffset.Hours)).Minutes,
                                Countries = _countriesService.GetCountriesByTimeZone(timeZone.DisplayName)
                            });

        return new FivePmModel()
        {
            NextTimeZones = nextTimeZones,
            PreviousTimeZones = previousTimeZones
        };
    }
}