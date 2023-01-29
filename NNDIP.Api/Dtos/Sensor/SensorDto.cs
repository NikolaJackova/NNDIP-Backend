using Newtonsoft.Json;
using NNDIP.Api.Dtos.Data;
using NNDIP.Api.Entities;

namespace NNDIP.Api.Dtos.Sensor
{
    public class SensorDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string SensorType { get; set; } = null!;
    }
}
