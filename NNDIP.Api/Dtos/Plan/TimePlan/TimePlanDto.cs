namespace NNDIP.Api.Dtos.Plan.TimePlan
{
    public class TimePlanDto
    {
        public long Id { get; set; }
        public TimeOnly FromTime { get; set; }
        public TimeOnly ToTime { get; set; }

        public virtual SimplePlanDto IdNavigation { get; set; } = null!;
    }
}
