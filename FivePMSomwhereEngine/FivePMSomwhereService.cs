using FivePMShared.Models;

namespace FivePMSomwhereEngine
{
    public class FivePmSomwhereService
    {
        public const int TargetHour = 17;
           
        public FivePmModel GetApplicableTimeZones()
        {
            var targetTime = new TimeSpan(TargetHour, 0, 0);
            
            var timeZones = TimeZoneInfo.GetSystemTimeZones();

            var currentDate = DateTime.UtcNow;

            var targetDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, TargetHour, 0, 0);

            int numberOfHours = TargetHour - currentDate.Hour;

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
                                           TimeAtOffset = currentDate.AddHours(timeZone.BaseUtcOffset.Hours),
                                           NumberOfMinutesAfterTarget = (currentDate.AddHours(timeZone.BaseUtcOffset.Hours) - targetDate).Minutes
                                       });

            var nextTimeZones = timeZones
                                .Where(timeZone => timeZone.BaseUtcOffset == firstUtcOffsetAfterTargetTime)
                                .Select(timeZone => new TimeBeforeTargetModel()
                                {
                                    TimeZoneName = timeZone.DisplayName,
                                    UtcOffset = timeZone.BaseUtcOffset.Hours,
                                    TimeAtOffset = currentDate.AddHours(timeZone.BaseUtcOffset.Hours),
                                    NumberOfMinutesBeforeTarget = (targetDate - currentDate.AddHours(timeZone.BaseUtcOffset.Hours)).Minutes
                                });

            return new FivePmModel()
            {
                NextTimeZones = nextTimeZones,
                PreviousTimezones = previousTimeZones
            };
        }
    }
}