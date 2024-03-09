using FivePMShared.Models;

namespace FivePMSomwhereEngine
{
    public class FivePMSomwhereService
    {
        public const int TargetHour = 17;
           
        public FivePMModel GetApplicableTimeZones()
        {
            var targetTime = new TimeSpan(TargetHour, 0, 0);
            
            var timeZones = TimeZoneInfo.GetSystemTimeZones();

            int numberOfHours = TargetHour - DateTime.UtcNow.Hour;

            if (timeZones is null)
            {
                return null;
            }

            var targetHourTimeZones = timeZones
                           .Where(timeZone => timeZone.BaseUtcOffset.Hours == numberOfHours)
                           .ToList();

            var firstUtcOffsetAfterTargetTime = timeZones
                                                    .Where(timeZone => timeZone.BaseUtcOffset.Hours > numberOfHours)
                                                    .GroupBy(timeZone => timeZone.BaseUtcOffset)
                                                    .OrderByDescending(TimeZone => TimeZone)
                                                    .Select(TimeZone => TimeZone)
                                                    .First();

            var nextTimeZonesToHitTargetTime = timeZones
                    .Where(timeZone => timeZone.BaseUtcOffset == firstUtcOffsetAfterTargetTime.Key)
                    .Select(timeZone => new TargetTimeModel()
                    {
                        TimeZoneName = timeZone.DisplayName,
                        UtcOffset = timeZone.BaseUtcOffset.Hours,
                        TimeAtOffset = DateTime.UtcNow.AddHours(timeZone.BaseUtcOffset.Hours),
                        NumberOfMinutesToFivePM = targetTime.Hours - DateTime.UtcNow.AddHours(timeZone.BaseUtcOffset.Hours).Hour
                    });

            return new FivePMModel()
            {
                TargetHourTimezones = targetHourTimeZones,
                TargetFivePMTimezones = nextTimeZonesToHitTargetTime.ToList()
            };
        }
    }
}