using NNDIP.Api.Dtos.Data;

namespace NNDIP.Api.Dtos.Sensor
{
    public class SensorsDataDto
    {
        public SensorsDataDto()
        {
            Data = new HashSet<SimpleDataDto>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string SensorType { get; set; } = null!;
        public DateTime DataTimestamp { get; set; }
        public virtual ICollection<SimpleDataDto> Data { get; set; }
    }
}
