namespace FivePMShared.Models;

public class FivePmModel
{
    public IEnumerable<TimeAfterTargetModel> PreviousTimezones { get; set; } = null!;
    public IEnumerable<TimeBeforeTargetModel> NextTimeZones { get; set; } = null!;
}
