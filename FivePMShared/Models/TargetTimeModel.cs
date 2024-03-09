namespace FivePMShared.Models
{
    public class TargetTimeModel
    {
        public string TimeZoneName { get; set; } = string.Empty;
        public int UtcOffset { get; set; }
        public DateTime TimeAtOffset { get; set; }
    }
}