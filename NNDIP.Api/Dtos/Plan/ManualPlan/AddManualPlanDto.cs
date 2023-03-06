namespace NNDIP.Api.Dtos.Plan.ManualPlan
{
    public class AddManualPlanDto
    {
        public long Id { get; set; }

        public virtual AddPlanDto IdNavigation { get; set; } = null!;
    }
}
