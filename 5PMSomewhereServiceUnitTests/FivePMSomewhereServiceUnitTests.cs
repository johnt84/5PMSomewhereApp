using FivePMShared.Constants;
using FivePMShared.Models;
using FivePMSomewhereEngine;
using FluentAssertions;
using NSubstitute;

namespace FivePMSomewhereUnitTests
{
    [TestClass]
    public class FivePMSomewhereServiceUnitTests
    {
        private ICountriesService _countriesService;

        private const int AustraliaTargetHour = 7;
        private const int USTargetHour = 22;
        private const int NumberOfMinutesInAnHour = 60;

        public FivePMSomewhereServiceUnitTests()
        {
            _countriesService = Substitute.For<ICountriesService>();
        }

        [TestMethod]
        public void TimeAtTargetDateAtGMTTimeZone_CurrentTimeZonesReturnedOnlyAndCountriesContainsUnitedKingdom()
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
            var applicableTimeZones = CallService(targetDate);

            // Assert
            applicableTimeZones.CurrentTimeZones.Should().NotBeNull();
            applicableTimeZones.PreviousTimeZones.Should().BeNull();
            applicableTimeZones.NextTimeZones.Should().BeNull();

            var timeZone = applicableTimeZones.CurrentTimeZones
                                .Where(timeZone => timeZone.TimeZoneName == TimeZoneNames.GMTTimeZoneName)
                                .First();

            timeZone.Countries.Contains(Countries.UnitedKingdom).Should().BeTrue();
            timeZone.RandomCountry.Should().Be(Countries.UnitedKingdom);
        }

        [TestMethod]
        public void TimeBeforeTargetDateWithGMTTimeZone_PreviousAndNextTimeZonesReturnedAndNextCountriesContainsUnitedKingdom()
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
            var applicableTimeZones = CallService(targetDate);

            // Assert
            applicableTimeZones.CurrentTimeZones.Should().BeNull();
            applicableTimeZones.PreviousTimeZones.Should().NotBeNull();
            applicableTimeZones.NextTimeZones.Should().NotBeNull();

            var timeZone = applicableTimeZones.NextTimeZones
                                .Where(timeZone => timeZone.TimeZoneName == TimeZoneNames.GMTTimeZoneName)
                                .First();

            timeZone.Countries.Contains(Countries.UnitedKingdom).Should().BeTrue();
            timeZone.RandomCountry.Should().Be(Countries.UnitedKingdom);
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
            var applicableTimeZones = CallService(targetDate);

            // Assert
            applicableTimeZones.CurrentTimeZones.Should().BeNull();
            applicableTimeZones.PreviousTimeZones.Should().NotBeNull();
            applicableTimeZones.NextTimeZones.Should().NotBeNull();

            var timeZone = applicableTimeZones.PreviousTimeZones
                                .Where(timeZone => timeZone.TimeZoneName == TimeZoneNames.GMTTimeZoneName)
                                .First();

            timeZone.NumberOfMinutesAfterTarget.Should().Be(numberOfMinutesAfterTarget);
            timeZone.Countries.Contains(Countries.UnitedKingdom).Should().BeTrue();
        }

        [TestMethod]
        public void TimeAtTargetDateWithAustralianTimeZone_CurrentTimeZonesReturnedOnlyAndCountriesContainsAustralia()
        {
            // Arrange
            var currentDate = DateTime.UtcNow;

            var targetDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, AustraliaTargetHour, 0, 0);

            var countriesList = new List<string>()
            {
                Countries.Australia
            };

            _countriesService.GetCountriesByTimeZone(TimeZoneNames.AustralianTimeZoneName).Returns(countriesList);
            _countriesService.GetRandomCountryByTimeZone(TimeZoneNames.AustralianTimeZoneName).Returns(Countries.Australia);

            // Act
            var applicableTimeZones = CallService(targetDate);

            // Assert
            applicableTimeZones.CurrentTimeZones.Should().NotBeNull();
            applicableTimeZones.PreviousTimeZones.Should().BeNull();
            applicableTimeZones.NextTimeZones.Should().BeNull();

            var timeZone = applicableTimeZones.CurrentTimeZones
                                .Where(timeZone => timeZone.TimeZoneName == TimeZoneNames.AustralianTimeZoneName)
                                .First();

            timeZone.Countries.Contains(Countries.Australia).Should().BeTrue();
            timeZone.RandomCountry.Should().Be(Countries.Australia);
        }

        //[TestMethod]
        //public void TimeBeforeTargetDateWithAustralianTimeZone_PreviousAndNextTimeZonesReturnedAndNextCountriesContainsAustralia()
        //{
        //    // Arrange
        //    const int numberOfMinutesBeforeTarget = 10;

        //    var currentDate = DateTime.UtcNow;

        //    int offsetHours = AustraliaTargetHour - 1;
        //    int offsetMinutes = NumberOfMinutesInAnHour - numberOfMinutesBeforeTarget;

        //    var targetDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, offsetHours, offsetMinutes, 0);

        //    var countriesList = new List<string>()
        //    {
        //        Countries.Australia
        //    };

        //    _countriesService.GetCountriesByTimeZone(TimeZoneNames.AustralianTimeZoneName).Returns(countriesList);
        //    _countriesService.GetRandomCountryByTimeZone(TimeZoneNames.AustralianTimeZoneName).Returns(Countries.Australia);

        //    // Act
        //    var applicableTimeZones = CallService(targetDate);

        //    // Assert
        //    applicableTimeZones.CurrentTimeZones.Should().BeNull();
        //    applicableTimeZones.PreviousTimeZones.Should().NotBeNull();
        //    applicableTimeZones.NextTimeZones.Should().NotBeNull();

        //    var timeZone = applicableTimeZones.NextTimeZones
        //                        .Where(timeZone => timeZone.TimeZoneName == TimeZoneNames.AustralianTimeZoneName)
        //                        .First();

        //    timeZone.Countries.Contains(Countries.Australia).Should().BeTrue();
        //    timeZone.RandomCountry.Should().Be(Countries.Australia);
        //}

        [TestMethod]
        public void TimeAfterTargetDateWithAustralianTimeZone_PreviousAndNextTimeZonesReturnedAndPreviousCountriesContainsAustralia()
        {
            // Arrange
            const int numberOfMinutesAfterTarget = 10;

            var currentDate = DateTime.UtcNow;

            var targetDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, AustraliaTargetHour, numberOfMinutesAfterTarget, 0);

            var countriesList = new List<string>()
            {
                Countries.Australia
            };

            _countriesService.GetCountriesByTimeZone(TimeZoneNames.AustralianTimeZoneName).Returns(countriesList);
            _countriesService.GetRandomCountryByTimeZone(TimeZoneNames.AustralianTimeZoneName).Returns(Countries.Australia);

            // Act
            var applicableTimeZones = CallService(targetDate);

            // Assert
            applicableTimeZones.CurrentTimeZones.Should().BeNull();
            applicableTimeZones.PreviousTimeZones.Should().NotBeNull();
            applicableTimeZones.NextTimeZones.Should().NotBeNull();

            var timeZone = applicableTimeZones.PreviousTimeZones
                                .Where(timeZone => timeZone.TimeZoneName == TimeZoneNames.AustralianTimeZoneName)
                                .First();

            timeZone.NumberOfMinutesAfterTarget.Should().Be(numberOfMinutesAfterTarget);    
            timeZone.Countries.Contains(Countries.Australia).Should().BeTrue();
            timeZone.RandomCountry.Should().Be(Countries.Australia);
        }

        [TestMethod]
        public void TimeAtTargetDateWithUSTimeZone_CurrentTimeZonesReturnedOnlyAndCountriesContainsUSA()
        {
            // Arrange
            var currentDate = DateTime.UtcNow;

            var targetDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, USTargetHour, 0, 0);

            var countriesList = new List<string>()
            {
                Countries.USA
            };

            _countriesService.GetCountriesByTimeZone(TimeZoneNames.USTimeZoneName).Returns(countriesList);
            _countriesService.GetRandomCountryByTimeZone(TimeZoneNames.USTimeZoneName).Returns(Countries.USA);

            // Act
            var applicableTimeZones = CallService(targetDate);

            // Assert
            applicableTimeZones.CurrentTimeZones.Should().NotBeNull();
            applicableTimeZones.PreviousTimeZones.Should().BeNull();
            applicableTimeZones.NextTimeZones.Should().BeNull();

            var timeZone = applicableTimeZones.CurrentTimeZones
                                .Where(timeZone => timeZone.TimeZoneName == TimeZoneNames.USTimeZoneName)
                                .First();

            timeZone.Countries.Contains(Countries.USA).Should().BeTrue();
            timeZone.RandomCountry.Should().Be(Countries.USA);
        }

        [TestMethod]
        public void TimeBeforeTargetDateWithUSTimeZone_PreviousAndNextTimeZonesReturnedAndNextCountriesContainsUSA()
        {
            // Arrange
            const int numberOfMinutesBeforeTarget = 10;

            var currentDate = DateTime.UtcNow;

            int offsetHours = USTargetHour - 1;
            int offsetMinutes = NumberOfMinutesInAnHour - numberOfMinutesBeforeTarget;

            var targetDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, offsetHours, offsetMinutes, 0);

            var countriesList = new List<string>()
            {
                Countries.USA
            };

            _countriesService.GetCountriesByTimeZone(TimeZoneNames.USTimeZoneName).Returns(countriesList);
            _countriesService.GetRandomCountryByTimeZone(TimeZoneNames.USTimeZoneName).Returns(Countries.USA);

            // Act
            var applicableTimeZones = CallService(targetDate);

            // Assert
            applicableTimeZones.CurrentTimeZones.Should().BeNull();
            applicableTimeZones.PreviousTimeZones.Should().NotBeNull();
            applicableTimeZones.NextTimeZones.Should().NotBeNull();

            var timeZone = applicableTimeZones.NextTimeZones
                                .Where(timeZone => timeZone.TimeZoneName == TimeZoneNames.USTimeZoneName)
                                .First();

            timeZone.Countries.Contains(Countries.USA).Should().BeTrue();
            timeZone.RandomCountry.Should().Be(Countries.USA);
        }

        [TestMethod]
        public void TimeAfterTargetDateWithUSTimeZone_PreviousAndNextTimeZonesReturnedAndPreviousCountriesContainsUSA()
        {
            // Arrange
            const int numberOfMinutesAfterTarget = 10;

            var currentDate = DateTime.UtcNow;

            var targetDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, USTargetHour, numberOfMinutesAfterTarget, 0);

            var countriesList = new List<string>()
            {
                Countries.USA
            };

            _countriesService.GetCountriesByTimeZone(TimeZoneNames.USTimeZoneName).Returns(countriesList);
            _countriesService.GetRandomCountryByTimeZone(TimeZoneNames.USTimeZoneName).Returns(Countries.USA);

            // Act
            var applicableTimeZones = CallService(targetDate);

            // Assert
            applicableTimeZones.CurrentTimeZones.Should().BeNull();
            applicableTimeZones.PreviousTimeZones.Should().NotBeNull();
            applicableTimeZones.NextTimeZones.Should().NotBeNull();

            var timeZone = applicableTimeZones.PreviousTimeZones
                                .Where(timeZone => timeZone.TimeZoneName == TimeZoneNames.USTimeZoneName)
                                .First();

            timeZone.NumberOfMinutesAfterTarget.Should().Be(numberOfMinutesAfterTarget);
            timeZone.Countries.Contains(Countries.USA).Should().BeTrue();
            timeZone.RandomCountry.Should().Be(Countries.USA);
        }

        private FivePmModel CallService(DateTime searchDate)
        {
            var fivePMSomewhereService = new FivePMSomewhereService(_countriesService);

            return fivePMSomewhereService.GetApplicableTimeZones(searchDate: searchDate);
        }
    }
}