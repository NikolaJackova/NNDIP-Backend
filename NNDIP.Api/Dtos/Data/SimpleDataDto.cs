using NNDIP.Api.Enums;

namespace NNDIP.Api.Dtos.Data
{
    public class SimpleDataDto
    {
        public SensorDataType Type { get; set; }
        public string TypeName { get => Type.ToString(); }
        public double? Value { get; set; }
    }
}
