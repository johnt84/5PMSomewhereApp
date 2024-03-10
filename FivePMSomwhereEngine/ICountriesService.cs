namespace FivePMSomwhereEngine
{
    public interface ICountriesService
    {
        IEnumerable<string> GetCountriesByTimeZone(string timeZoneName);
    }
}
