namespace FivePMSomewhereShared.Constants
{
    public static class TimeZoneNames
    {
        public const string GMTTimeZoneName = "(UTC) Coordinated Universal Time";
        public const string AustralianTimeZoneName = "(UTC+10:00) Canberra, Melbourne, Sydney";
        public const string USTimeZoneName = "(UTC-05:00) Eastern Time (US & Canada)";

        public static List<string> Values => new List<string>()
        {
            GMTTimeZoneName,
            AustralianTimeZoneName,
            USTimeZoneName
        };
    }
}
