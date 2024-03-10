using RESTCountries.NET.Models;
using RESTCountries.NET.Services;

namespace FivePMSomwhereEngine
{
    public class CountriesService : ICountriesService
    {
        private readonly IEnumerable<Country> _countries;

        public CountriesService()
        {
            _countries = RestCountriesService.GetAllCountries();
        }

        public IEnumerable<string> GetCountriesByTimeZone(string timeZoneName)
        {
            string timeZoneNameForCountrySearch = GetTimeZoneForCountrySearch(timeZoneName);

            return _countries
                    .Where(country => country.Timezones.ToList().Contains(timeZoneNameForCountrySearch))
                    .Select(country => country.Name.Official);
        }

        private string GetTimeZoneForCountrySearch(string timeZoneName)
        {
            var timeZoneNameSplit = timeZoneName.Split(")").ToList();

            return timeZoneNameSplit[0].Replace("(", string.Empty);
        }
    }
}
