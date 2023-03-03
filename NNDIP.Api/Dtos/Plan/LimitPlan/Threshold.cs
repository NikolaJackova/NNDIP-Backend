namespace NNDIP.Api.Dtos.Plan.LimitPlan
{
    public class Threshold
    {
        public long Id { get; set; }
        public double Value { get; set; }
        public long EventId { get; set; }
        public ulong Enabled { get; set; }
    }
}
