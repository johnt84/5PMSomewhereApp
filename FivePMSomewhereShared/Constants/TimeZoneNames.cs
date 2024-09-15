namespace FivePMSomewhereShared.Constants
{
    public static class TimeZoneNames
    {
        public const string GMTTimeZoneName = "(UTC+00:00) Dublin, Edinburgh, Lisbon, London";
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
