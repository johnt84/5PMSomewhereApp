namespace FivePMSomewhereShared.Models;

public class FivePmModel
{
    public IEnumerable<TargetTimeModel> CurrentTimeZones { get; set; } = null!;
    public IEnumerable<TimeAfterTargetModel> PreviousTimeZones { get; set; } = null!;
    public IEnumerable<TimeBeforeTargetModel> NextTimeZones { get; set; } = null!;
}
