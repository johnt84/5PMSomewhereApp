namespace FivePMSomewhereShared.Models;

public class TargetTimeModel
{
    public string TimeZoneName { get; set; } = string.Empty;
    public int UtcOffset { get; set; }
    public DateTime TimeAtOffset { get; set; }
    public IEnumerable<string> Countries { get; set; } = null!;
    public string RandomCountry { get; set; } = string.Empty;
    public bool SelectedTimeZone { get; set; }
}