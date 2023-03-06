namespace NNDIP.Api.Dtos.Plan.ManualPlan
{
    public class ManualPlanDto
    {
        public long Id { get; set; }

        public virtual SimplePlanDto IdNavigation { get; set; } = null!;
    }
}
