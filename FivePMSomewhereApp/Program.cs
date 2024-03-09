using FivePMSomwhereEngine;

var fivePMSomewhereService = new FivePmSomwhereService();

var applicableTimeZones = fivePMSomewhereService.GetApplicableTimeZones();

var firstPreviousTimeZone = applicableTimeZones.PreviousTimezones.First();

int numberOfMinutesAfterTarget = firstPreviousTimeZone.NumberOfMinutesAfterTarget;
var timeAtTimeZoneAfterTarget = firstPreviousTimeZone.TimeAtOffset;

Console.WriteLine($"TimeZones {numberOfMinutesAfterTarget} minutes after 5 PM");
Console.WriteLine($"\nTime is {timeAtTimeZoneAfterTarget}");

foreach (var timeZone in applicableTimeZones.PreviousTimezones)
{
    Console.WriteLine($"\nTimeZone {timeZone.TimeZoneName}");
}

var firstNextTimeZone = applicableTimeZones.NextTimeZones.First();

int numberOfMinutesBeforeTarget = firstNextTimeZone.NumberOfMinutesBeforeTarget;
var timeAtTimeZoneBeforeTarget = firstNextTimeZone.TimeAtOffset;

Console.WriteLine($"\n\nTimeZones {numberOfMinutesBeforeTarget} minutes before 5 PM");
Console.WriteLine($"\nTime is {timeAtTimeZoneBeforeTarget}");

foreach (var timeZone in applicableTimeZones.NextTimeZones)
{
    Console.WriteLine($"\nTimeZone {timeZone.TimeZoneName}");
}


Console.WriteLine("\nPress any key to continue..");
Console.ReadLine();