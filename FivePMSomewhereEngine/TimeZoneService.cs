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

    public TimeZoneModel? GetSelectedTimeZones(DateTime? searchDate = null, string? currentCountry = null, string? selectedTimeZoneName = null
        , string? selectedCountryName = null)
    {
        var applicableTimeZones = _fivePMSomewhereService.GetApplicableTimeZones(searchDate: searchDate, currentCountry: currentCountry
                                            , selectedTimeZoneName: selectedTimeZoneName, selectedCountryName: selectedCountryName);

        if (applicableTimeZones is null)
        {
            return null;
        }

        var currentTimeZone = GetCurrentTimeZone(applicableTimeZones.CurrentTimeZones, selectedTimeZoneName: selectedTimeZoneName);

        var previousTimeZone = GetPreviousTimeZone(applicableTimeZones.PreviousTimeZones, selectedTimeZoneName: selectedTimeZoneName);

        var nextTimeZone = GetNextTimeZone(applicableTimeZones.NextTimeZones);

        return new TimeZoneModel
        {
            CurrentDate = DateTime.UtcNow,
            CurrentTimeZone = currentTimeZone,
            PreviousTimeZone = previousTimeZone,
            NextTimeZone = nextTimeZone
        };
    }

    private TargetTimeModel? GetCurrentTimeZone(IEnumerable<TargetTimeModel> currentTimeZones, string? selectedTimeZoneName = null)
    {
        if (currentTimeZones is null || !currentTimeZones.Any())
        {
            return null;
        }

        return GetSelectedCurrentTimeZone(currentTimeZones, selectedTimeZoneName: selectedTimeZoneName);
    }

    private TimeAfterTargetModel? GetPreviousTimeZone(IEnumerable<TimeAfterTargetModel> previousTimeZones, string? selectedTimeZoneName = null)
    {
        if (previousTimeZones is null || !previousTimeZones.Any())
        {
            return null;
        }

        return GetSelectedPreviousTimeZone(previousTimeZones, selectedTimeZoneName: selectedTimeZoneName);
    }

    private TimeBeforeTargetModel? GetNextTimeZone(IEnumerable<TimeBeforeTargetModel> nextTimeZones)
    {
        if (nextTimeZones is null || !nextTimeZones.Any())
        {
            return null;
        }

        return GetSelectedNextTimeZone(nextTimeZones);
    }

    private TargetTimeModel GetSelectedCurrentTimeZone(IEnumerable<TargetTimeModel> currentTimeZones, string? selectedTimeZoneName = null)
    {
        if (currentTimeZones.Count() == 1)
        {
            return currentTimeZones.Single();
        }
        else
        {
            var selectedTimeZone = currentTimeZones
                                           .FirstOrDefault(currentTimeZone => currentTimeZone.TimeZoneName == selectedTimeZoneName);

            if (selectedTimeZone is not null)
            {
                return selectedTimeZone;
            }

            var selectedCurrentTimeZone = currentTimeZones
                                            .FirstOrDefault(currentTimeZone => TimeZoneNames.Values.Contains(currentTimeZone.TimeZoneName));

            if (selectedCurrentTimeZone is not null)
            {
                return selectedCurrentTimeZone;
            }

            var random = new Random();

            int randomPosition = random.Next(currentTimeZones.Count());

            return currentTimeZones.ToArray()[randomPosition];
        }
    }

    private TimeAfterTargetModel? GetSelectedPreviousTimeZone(IEnumerable<TimeAfterTargetModel> previousTimeZones, string? selectedTimeZoneName = null)
    {
        if (previousTimeZones.Count() == 1)
        {
            return previousTimeZones.Single();
        }
        else
        {
            var selectedTimeZone = previousTimeZones
                                    .FirstOrDefault(previousTimeZone => previousTimeZone.TimeZoneName == selectedTimeZoneName);

            if (selectedTimeZone is not null)
            {
                return selectedTimeZone;
            }

            var selectedPreviousTimeZone = previousTimeZones
                                            .FirstOrDefault(previousTimeZone => TimeZoneNames.Values.Contains(previousTimeZone.TimeZoneName));

            if (selectedPreviousTimeZone is not null)
            {
                return selectedPreviousTimeZone;
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
