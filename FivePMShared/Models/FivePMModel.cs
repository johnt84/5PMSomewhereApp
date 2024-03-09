namespace FivePMShared.Models
{
    public class FivePmModel
    {
        public IEnumerable<TimeAfterTargetModel> PreviousTimezones { get; set; }
        public IEnumerable<TimeBeforeTargetModel> NextTimeZones { get; set; }
    }
}
