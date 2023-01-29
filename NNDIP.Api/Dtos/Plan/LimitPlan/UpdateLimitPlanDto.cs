namespace NNDIP.Api.Dtos.Plan.LimitPlan
{
    public class UpdateLimitPlanDto
    {
        public long Id { get; set; }
        public double OptimalValue { get; set; }
        public double ThresholdValue { get; set; }

        public virtual UpdatePlanDto IdNavigation { get; set; } = null!;
    }
}
