namespace NNDIP.Api.Dtos.Plan.ManualPlan
{
    public class UpdateManualPlanDto
    {
        public long Id { get; set; }

        public virtual UpdatePlanDto IdNavigation { get; set; } = null!;
    }
}
