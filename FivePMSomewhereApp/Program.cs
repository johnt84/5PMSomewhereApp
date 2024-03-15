using FivePMSomewhereEngine;

var countriesService = new CountriesService();

var fivePMSomewhereService = new FivePMSomewhereService(countriesService);

var currentDate = DateTime.UtcNow;

//var targetDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 17, 10, 0);

var applicableTimeZones = fivePMSomewhereService.GetApplicableTimeZones(currentDate);

if (applicableTimeZones.CurrentTimeZones is not null)
{
    Console.WriteLine("It is currently 5 PM at...");

    foreach (var timeZone in applicableTimeZones.CurrentTimeZones)
    {
        Console.WriteLine($"\nTimeZone {timeZone.TimeZoneName}");

        Console.WriteLine($"Countries - {string.Join(", ", timeZone.Countries)}");
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
    }
}

Console.WriteLine("\nPress any key to continue..");
Console.ReadLine();