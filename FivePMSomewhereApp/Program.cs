﻿using FivePMSomewhereEngine;

var countriesService = new CountriesService();

var fivePMSomewhereService = new FivePMSomewhereService(countriesService);

var applicableTimeZones = fivePMSomewhereService.GetApplicableTimeZones(DateTime.UtcNow);

var firstPreviousTimeZone = applicableTimeZones.PreviousTimezones.First();

int numberOfMinutesAfterTarget = firstPreviousTimeZone.NumberOfMinutesAfterTarget;
var timeAtTimeZoneAfterTarget = firstPreviousTimeZone.TimeAtOffset;

Console.WriteLine($"It was 5 PM in these TimeZones {numberOfMinutesAfterTarget} minutes ago");
Console.WriteLine($"\nTime is {timeAtTimeZoneAfterTarget}");

foreach (var timeZone in applicableTimeZones.PreviousTimezones)
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


Console.WriteLine("\nPress any key to continue..");
Console.ReadLine();