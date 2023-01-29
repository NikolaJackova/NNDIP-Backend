using System;
using System.Collections.Generic;

namespace NNDIP.Api.Entities
{
    public partial class LimitPlan
    {
        public long Id { get; set; }
        public double OptimalValue { get; set; }
        public double ThresholdValue { get; set; }
        public string ValueType { get; set; } = null!;
        public long YearPeriodId { get; set; }
        public ulong? Active { get; set; }
        public DateTime? LastTriggered { get; set; }

        public virtual Plan IdNavigation { get; set; } = null!;
        public virtual YearPeriod YearPeriod { get; set; } = null!;
    }
}
