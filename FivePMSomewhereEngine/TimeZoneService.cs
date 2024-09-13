using FivePMSomewhereShared.Constants;
using FivePMSomewhereShared.Models;

namespace FivePMSomewhereEngine;

public class TimeZoneService : ITimeZoneService
{
    private readonly IFivePMSomewhereService _fivePMSomewhereService;

    public TimeZoneService(IFivePMSomewhereService FivePMSomewhereService)
    {
        _fivePMSomewhereService = FivePMSomewhereService;
    }

    public TimeZoneModel? GetSelectedTimeZones(DateTime? searchDate = null, string? currentCountry = null)
    {
        var applicableTimeZones = _fivePMSomewhereService.GetApplicableTimeZones(searchDate: searchDate, currentCountry: currentCountry);

        if (applicableTimeZones is null)
        {
            return null;
        }

        var currentTimeZone = GetCurrentTimeZone(applicableTimeZones.CurrentTimeZones);

        var previousTimeZone = GetPreviousTimeZone(applicableTimeZones.PreviousTimeZones);

        var nextTimeZone = GetNextTimeZone(applicableTimeZones.NextTimeZones);

        return new TimeZoneModel
        {
            CurrentDate = DateTime.Now,
            CurrentTimeZone = currentTimeZone,
            PreviousTimeZone = previousTimeZone,
            NextTimeZone = nextTimeZone
        };
    }

    private TargetTimeModel? GetCurrentTimeZone(IEnumerable<TargetTimeModel> currentTimeZones)
    {
        if (currentTimeZones is null || !currentTimeZones.Any())
        {
            return null;
        }

        return GetSelectedCurrentTimeZone(currentTimeZones);
    }

    private TimeAfterTargetModel? GetPreviousTimeZone(IEnumerable<TimeAfterTargetModel> previousTimeZones)
    {
        if (previousTimeZones is null || !previousTimeZones.Any())
        {
            return null;
        }

        return GetSelectedPreviousTimeZone(previousTimeZones);
    }

    private TimeBeforeTargetModel? GetNextTimeZone(IEnumerable<TimeBeforeTargetModel> nextTimeZones)
    {
        if (nextTimeZones is null || !nextTimeZones.Any())
        {
            return null;
        }

        return GetSelectedNextTimeZone(nextTimeZones);
    }

    private TargetTimeModel GetSelectedCurrentTimeZone(IEnumerable<TargetTimeModel> currentTimeZones)
    {
        if (currentTimeZones.Count() == 1)
        {
            return currentTimeZones.Single();
        }
        else
        {
            var selectedTimeZone = currentTimeZones
                                    .FirstOrDefault(nextTimeZone => TimeZoneNames.Values.Contains(nextTimeZone.TimeZoneName));

            if (selectedTimeZone is not null)
            {
                return selectedTimeZone;
            }

            var random = new Random();

            int randomPosition = random.Next(currentTimeZones.Count());

            return currentTimeZones.ToArray()[randomPosition];
        }
    }

    private TimeAfterTargetModel? GetSelectedPreviousTimeZone(IEnumerable<TimeAfterTargetModel> previousTimeZones)
    {
        if (previousTimeZones.Count() == 1)
        {
            return previousTimeZones.Single();
        }
        else
        {
            var selectedTimeZone = previousTimeZones
                                    .FirstOrDefault(nextTimeZone => TimeZoneNames.Values.Contains(nextTimeZone.TimeZoneName));

            if (selectedTimeZone is not null)
            {
                return selectedTimeZone;
            }

            var random = new Random();

            int randomPosition = random.Next(previousTimeZones.Count());

            return previousTimeZones.ToArray()[randomPosition];
        }
    }

    private TimeBeforeTargetModel? GetSelectedNextTimeZone(IEnumerable<TimeBeforeTargetModel> nextTimeZones)
    {
        if (nextTimeZones.Count() == 1)
        {
            return nextTimeZones.Single();
        }
        else
        {
            var selectedTimeZone = nextTimeZones
                                    .FirstOrDefault(nextTimeZone => TimeZoneNames.Values.Contains(nextTimeZone.TimeZoneName));

            if (selectedTimeZone is not null)
            {
                return selectedTimeZone;
            }
            
            var random = new Random();

            int randomPosition = random.Next(nextTimeZones.Count());

            return nextTimeZones.ToArray()[randomPosition];
        }
    }
}
