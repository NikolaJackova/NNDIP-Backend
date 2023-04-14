namespace NNDIP.Api.Entities
{
    public partial class Plan
    {
        public long Id { get; set; }
        public ulong Enabled { get; set; }
        public string Name { get; set; } = null!;
        public long EventId { get; set; }
        public int? Priority { get; set; }
        public string PlanType { get; set; } = null!;

        public virtual Event Event { get; set; } = null!;
        public virtual GpioPlan GpioPlan { get; set; } = null!;
        public virtual LimitPlan LimitPlan { get; set; } = null!;
        public virtual ManualPlan ManualPlan { get; set; } = null!;
        public virtual TimePlan TimePlan { get; set; } = null!;
    }
}
