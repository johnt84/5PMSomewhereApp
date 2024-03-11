using FivePMShared.Models;

namespace FivePMSomewhereEngine;

public class FivePMSomewhereService
{
    private readonly ICountriesService _countriesService;

    public FivePMSomewhereService(ICountriesService countriesService)
    {
        _countriesService = countriesService;
    }

    public const int TargetHour = 17;
           
    public FivePmModel GetApplicableTimeZones(DateTime searchDate)
    {
        var targetTime = new TimeSpan(TargetHour, 0, 0);
            
        var timeZones = TimeZoneInfo.GetSystemTimeZones();

        var targetDate = new DateTime(searchDate.Year, searchDate.Month, searchDate.Day, TargetHour, 0, 0);

        int numberOfHours = TargetHour - searchDate.Hour;

        if (timeZones is null)
        {
            return null;
        }

        var targetHourTimeZones = timeZones
                        .Where(timeZone => timeZone.BaseUtcOffset.Hours == numberOfHours);

        var firstUtcOffsetAfterTargetTime = timeZones
                                                .Where(timeZone => timeZone.BaseUtcOffset.Hours < numberOfHours)
                                                .GroupBy(timeZone => timeZone.BaseUtcOffset)
                                                .OrderByDescending(TimeZone => TimeZone.Key)
                                                .Select(TimeZone => TimeZone.Key)
                                                .First();

        var previousTimeZones = targetHourTimeZones
                                    .Select(timeZone => new TimeAfterTargetModel()
                                    {
                                        TimeZoneName = timeZone.DisplayName,
                                        UtcOffset = timeZone.BaseUtcOffset.Hours,
                                        TimeAtOffset = searchDate.AddHours(timeZone.BaseUtcOffset.Hours),
                                        NumberOfMinutesAfterTarget = (searchDate.AddHours(timeZone.BaseUtcOffset.Hours) - targetDate).Minutes,
                                        Countries = _countriesService.GetCountriesByTimeZone(timeZone.DisplayName)
                                    });

        var nextTimeZones = timeZones
                            .Where(timeZone => timeZone.BaseUtcOffset == firstUtcOffsetAfterTargetTime)
                            .Select(timeZone => new TimeBeforeTargetModel()
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
            PreviousTimezones = previousTimeZones
        };
    }
}