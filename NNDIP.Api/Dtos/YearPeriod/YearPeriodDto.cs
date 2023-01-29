using NNDIP.Api.Dtos.Plan.LimitPlan;
using NNDIP.Api.Entities;

namespace NNDIP.Api.Dtos.YearPeriod
{
    public class YearPeriodDto
    {
        public YearPeriodDto()
        {
            LimitPlans = new HashSet<LimitPlanDto>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ulong Active { get; set; }
        public virtual ICollection<LimitPlanDto> LimitPlans { get; set; }
    }
}
