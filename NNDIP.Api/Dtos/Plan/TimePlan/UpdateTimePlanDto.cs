namespace NNDIP.Api.Dtos.Plan.TimePlan
{
    public class UpdateTimePlanDto
    {
        public long Id { get; set; }
        public TimeOnly FromTime { get; set; }
        public TimeOnly ToTime { get; set; }

        public virtual UpdatePlanDto IdNavigation { get; set; } = null!;
    }
}
