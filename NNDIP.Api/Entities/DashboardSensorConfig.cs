using System;
using System.Collections.Generic;

namespace NNDIP.Api.Entities
{
    public partial class DashboardSensorConfig
    {
        public long Id { get; set; }
        public string MeasuredValueType { get; set; } = null!;
        public long SensorId { get; set; }

        public virtual Sensor Sensor { get; set; } = null!;
    }
}
