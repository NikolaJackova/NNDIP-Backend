using NNDIP.Api.Dtos.YearPeriod;

namespace NNDIP.Api.Dtos.Plan.LimitPlan
{
    public class LimitPlanDto
    {
        public long Id { get; set; }
        public double OptimalValue { get; set; }
        public double ThresholdValue { get; set; }
        public string ValueType { get; set; } = null!;
        public long YearPeriodId { get; set; }
        public ulong? Active { get; set; }
        public DateTime? LastTriggered { get; set; }

        public virtual SimplePlanDto IdNavigation { get; set; } = null!;
        public virtual SimpleYearPeriodDto YearPeriod { get; set; } = null!;
    }
}
