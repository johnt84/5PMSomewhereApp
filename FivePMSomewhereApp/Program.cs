using FivePMSomewhereEngine;
using FivePMSomewhereShared.Constants;

var countriesService = new CountriesService();

var fivePMSomewhereService = new FivePMSomewhereService(countriesService);

//var targetDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 17, 00, 0);

var applicableTimeZones = fivePMSomewhereService.GetApplicableTimeZones(currentCountry: null);

if (applicableTimeZones.CurrentTimeZones is not null)
{
    Console.WriteLine("It is currently 5 PM at...");

    foreach (var timeZone in applicableTimeZones.CurrentTimeZones)
    {
        Console.WriteLine($"\nTimeZone {timeZone.TimeZoneName}");

        Console.WriteLine($"Countries - {string.Join(", ", timeZone.Countries)}");

        Console.WriteLine($"First Country - {timeZone.RandomCountry}");
    }
}
else
{
    var firstPreviousTimeZone = applicableTimeZones.PreviousTimeZones.First();

    int numberOfMinutesAfterTarget = firstPreviousTimeZone.NumberOfMinutesAfterTarget;
    var timeAtTimeZoneAfterTarget = firstPreviousTimeZone.TimeAtOffset;

    Console.WriteLine($"It was 5 PM in these TimeZones {numberOfMinutesAfterTarget} minutes ago");
    Console.WriteLine($"\nTime is {timeAtTimeZoneAfterTarget}");

    foreach (var timeZone in applicableTimeZones.PreviousTimeZones)
    {
        Console.WriteLine($"\nTimeZone {timeZone.TimeZoneName}");

        Console.WriteLine($"Countries - {string.Join(", ", timeZone.Countries)}");

        Console.WriteLine($"Random Country - {timeZone.RandomCountry}");
    }

    var firstNextTimeZone = applicableTimeZones.NextTimeZones.First();

    int numberOfMinutesBeforeTarget = firstNextTimeZone.NumberOfMinutesBeforeTarget;
    var timeAtTimeZoneBeforeTarget = firstNextTimeZone.TimeAtOffset;

    Console.WriteLine($"\n\nWill be 5 PM in these TimeZones in {numberOfMinutesBeforeTarget} minutes");
    Console.WriteLine($"\nTime is {timeAtTimeZoneBeforeTarget}");

    foreach (var timeZone in applicableTimeZones.NextTimeZones)
    {
        Console.WriteLine($"\nTimeZone {timeZone.TimeZoneName}");

        Console.WriteLine($"Countries - {string.Join(", ", timeZone.Countries)}");

        Console.WriteLine($"Random Country - {timeZone.RandomCountry}");
    }
}

Console.WriteLine("\nPress any key to continue..");
Console.ReadLine();