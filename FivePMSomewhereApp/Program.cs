using FivePMSomewhereEngine;

var countriesService = new CountriesService();

var fivePMSomewhereService = new FivePMSomewhereService(countriesService);

var timeZoneService = new TimeZoneService(fivePMSomewhereService);

var selectedTimeZone = timeZoneService.GetSelectedTimeZones();

if (selectedTimeZone?.CurrentTimeZone is not null)
{
    Console.WriteLine("It is currently 5 PM at...");

    Console.WriteLine($"\nTimeZone {selectedTimeZone.CurrentTimeZone.TimeZoneName}");

    Console.WriteLine($"Countries - {string.Join(", ", selectedTimeZone.CurrentTimeZone.Countries)}");

    Console.WriteLine($"First Country - {selectedTimeZone.CurrentTimeZone.RandomCountry}");
}
else
{
    if (selectedTimeZone?.PreviousTimeZone is not null)
    {
        int numberOfMinutesAfterTarget = selectedTimeZone.PreviousTimeZone.NumberOfMinutesAfterTarget;
        var timeAtTimeZoneAfterTarget = selectedTimeZone.PreviousTimeZone.TimeAtOffset;

        Console.WriteLine($"It was 5 PM in these TimeZones {numberOfMinutesAfterTarget} minutes ago");
        Console.WriteLine($"\nTime is {timeAtTimeZoneAfterTarget}");

        Console.WriteLine($"\nTimeZone {selectedTimeZone.PreviousTimeZone.TimeZoneName}");

        Console.WriteLine($"Countries - {string.Join(", ", selectedTimeZone.PreviousTimeZone.Countries)}");

        Console.WriteLine($"Random Country - {selectedTimeZone.PreviousTimeZone.RandomCountry}");
    }


    if (selectedTimeZone?.NextTimeZone is not null)
    {
        int numberOfMinutesBeforeTarget = selectedTimeZone.NextTimeZone.NumberOfMinutesBeforeTarget;
        var timeAtTimeZoneBeforeTarget = selectedTimeZone.NextTimeZone.TimeAtOffset;

        Console.WriteLine($"\nWill be 5 PM in these TimeZones in {numberOfMinutesBeforeTarget} minutes");
        Console.WriteLine($"\nTime is {timeAtTimeZoneBeforeTarget}");

        Console.WriteLine($"\nTimeZone {selectedTimeZone.NextTimeZone.TimeZoneName}");

        Console.WriteLine($"Countries - {string.Join(", ", selectedTimeZone.NextTimeZone.Countries)}");

        Console.WriteLine($"Random Country - {selectedTimeZone.NextTimeZone.RandomCountry}");
    }
}

Console.WriteLine("\nPress any key to continue..");
Console.ReadLine();