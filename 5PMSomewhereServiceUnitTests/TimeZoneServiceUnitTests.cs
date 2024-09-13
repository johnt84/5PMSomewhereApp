using FivePMSomewhereEngine;
using FivePMSomewhereShared.Constants;
using FivePMSomewhereShared.Models;
using FluentAssertions;
using NSubstitute;

namespace _5PMSomewhereServiceUnitTests;

[TestClass]
public class TimeZoneServiceUnitTests
{
    private ICountriesService _countriesService;

    private const int AustraliaTargetHour = 7;
    private const int USTargetHour = 22;
    private const int NumberOfMinutesInAnHour = 60;

    public TimeZoneServiceUnitTests()
    {
        _countriesService = Substitute.For<ICountriesService>();
    }

    [TestMethod]
    public void TimeAtTargetDateAtGMTTimeZone_CurrentTimeZonesReturnedOnlyCountryIsUnitedKingdomAndTimeZoneIsGMTTimeZone()
    {
        // Arrange
        var currentDate = DateTime.UtcNow;

        var targetDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, TargetTime.TargetHour, 0, 0);

        var countriesList = new List<string>()
        {
            Countries.UnitedKingdom
        };

        _countriesService.GetCountriesByTimeZone(TimeZoneNames.GMTTimeZoneName).Returns(countriesList);
        _countriesService.GetRandomCountryByTimeZone(TimeZoneNames.GMTTimeZoneName).Returns(Countries.UnitedKingdom);

        // Act
        var applicableTimeZone = CallService(targetDate);

        // Assert
        applicableTimeZone!.CurrentTimeZone!.Should().NotBeNull();
        applicableTimeZone!.PreviousTimeZone!.Should().BeNull();
        applicableTimeZone!.NextTimeZone!.Should().BeNull();

        applicableTimeZone!.CurrentTimeZone!.Countries.Contains(Countries.UnitedKingdom).Should().BeTrue();
        applicableTimeZone!.CurrentTimeZone!.TimeZoneName.Should().Be(TimeZoneNames.GMTTimeZoneName);
    }

    [TestMethod]
    public void TimeBeforeTargetDateWithGMTTimeZone_PreviousAndNextTimeZonesReturnedNextCountryIsUnitedKingdomAndNextTimeZoneIsGMTTimeZone()
    {
        // Arrange
        const int numberOfMinutesBeforeTarget = 10;

        var currentDate = DateTime.UtcNow;

        int offsetHours = TargetTime.TargetHour - 1;
        int offsetMinutes = NumberOfMinutesInAnHour - numberOfMinutesBeforeTarget;

        var targetDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, offsetHours, offsetMinutes, 0);

        var countriesList = new List<string>()
        {
            Countries.UnitedKingdom
        };

        _countriesService.GetCountriesByTimeZone(TimeZoneNames.GMTTimeZoneName).Returns(countriesList);
        _countriesService.GetRandomCountryByTimeZone(TimeZoneNames.GMTTimeZoneName).Returns(Countries.UnitedKingdom);

        // Act
        var applicableTimeZone = CallService(targetDate);

        // Assert
        applicableTimeZone!.CurrentTimeZone.Should().BeNull();
        applicableTimeZone!.PreviousTimeZone.Should().NotBeNull();
        applicableTimeZone!.NextTimeZone.Should().NotBeNull();

        applicableTimeZone!.NextTimeZone!.Countries.Contains(Countries.UnitedKingdom).Should().BeTrue();
        applicableTimeZone!.NextTimeZone!.RandomCountry.Should().Be(Countries.UnitedKingdom);
    }

    [TestMethod]
    public void TimeAfterTargetDateWithGMTimeZone_PreviousAndNextTimeZonesReturnedAndPreviousCountriesContainsUnitedKingdom()
    {
        // Arrange
        const int numberOfMinutesAfterTarget = 10;

        var currentDate = DateTime.UtcNow;

        var targetDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, TargetTime.TargetHour, numberOfMinutesAfterTarget, 0);

        var countriesList = new List<string>()
        {
            Countries.UnitedKingdom
        };

        _countriesService.GetCountriesByTimeZone(TimeZoneNames.GMTTimeZoneName).Returns(countriesList);
        _countriesService.GetRandomCountryByTimeZone(TimeZoneNames.GMTTimeZoneName).Returns(Countries.UnitedKingdom);

        // Act
        var applicableTimeZone = CallService(targetDate);

        // Assert
        applicableTimeZone!.CurrentTimeZone.Should().BeNull();
        applicableTimeZone!.PreviousTimeZone.Should().NotBeNull();
        applicableTimeZone!.NextTimeZone.Should().NotBeNull();

        applicableTimeZone!.PreviousTimeZone!.Countries.Contains(Countries.UnitedKingdom).Should().BeTrue();
        applicableTimeZone!.PreviousTimeZone!.TimeZoneName.Should().Be(TimeZoneNames.GMTTimeZoneName);
    }

    private TimeZoneModel? CallService(DateTime searchDate)
    {
        var fivePMSomewhereService = new FivePMSomewhereService(_countriesService);

        var timeZoneService = new TimeZoneService(fivePMSomewhereService);

        return timeZoneService.GetSelectedTimeZones(searchDate: searchDate);
    }
}
