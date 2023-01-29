using System;
using System.Collections.Generic;

namespace NNDIP.Api.Entities
{
    public partial class GpioPlan
    {
        public long Id { get; set; }
        public string PinName { get; set; } = null!;
        public int PinAddress { get; set; }
        public string DefaultState { get; set; } = null!;

        public virtual Plan IdNavigation { get; set; } = null!;
        public virtual ManualGpioPlan ManualGpioPlan { get; set; } = null!;
        public virtual TimeGpioPlan TimeGpioPlan { get; set; } = null!;
    }
}
