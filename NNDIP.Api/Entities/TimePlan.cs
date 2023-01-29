using System;
using System.Collections.Generic;

namespace NNDIP.Api.Entities
{
    public partial class TimePlan
    {
        public long Id { get; set; }
        public TimeOnly FromTime { get; set; }
        public TimeOnly ToTime { get; set; }

        public virtual Plan IdNavigation { get; set; } = null!;
    }
}
