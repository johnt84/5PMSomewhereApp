using FivePMShared.Models;
using FivePMSomewhereEngine;
using NSubstitute;

namespace FivePMSomewhereUnitTests
{
    [TestClass]
    public class FivePMSomewhereServiceUnitTests
    {
        private ICountriesService _countriesService;

        public FivePMSomewhereServiceUnitTests()
        {
            _countriesService = Substitute.For<ICountriesService>();
        }

        [TestMethod]
        public void TestMethod1()
        {
            var applicableTimeZones = CallService(DateTime.UtcNow);
        }

        private FivePmModel CallService(DateTime searchDate)
        {
            var fivePMSomewhereService = new FivePMSomewhereService(_countriesService);

            return fivePMSomewhereService.GetApplicableTimeZones(searchDate);
        }
    }
}