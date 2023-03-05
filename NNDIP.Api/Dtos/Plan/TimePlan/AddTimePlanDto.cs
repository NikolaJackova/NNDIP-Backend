namespace NNDIP.Api.Dtos.Plan.TimePlan
{
    public class AddTimePlanDto
    {
        public long Id { get; set; }
        public TimeOnly FromTime { get; set; }
        public TimeOnly ToTime { get; set; }

        public virtual AddPlanDto IdNavigation { get; set; } = null!;
    }
}
