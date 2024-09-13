namespace FivePMSomewhereShared.Models;

public class TimeZoneModel
{
    public DateTime CurrentDate { get; set; }
    public TargetTimeModel? CurrentTimeZone { get; set; }
    public TimeAfterTargetModel? PreviousTimeZone { get; set; } 
    public TimeBeforeTargetModel? NextTimeZone { get; set; } 
}
