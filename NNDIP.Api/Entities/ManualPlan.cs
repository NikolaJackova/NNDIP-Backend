using System;
using System.Collections.Generic;

namespace NNDIP.Api.Entities
{
    public partial class ManualPlan
    {
        public long Id { get; set; }

        public virtual Plan IdNavigation { get; set; } = null!;
    }
}
