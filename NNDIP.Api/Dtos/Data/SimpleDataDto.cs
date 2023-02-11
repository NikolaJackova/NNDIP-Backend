using NNDIP.Api.Enums;

namespace NNDIP.Api.Dtos.Data
{
    public class SimpleDataDto
    {
        public SensorDataType Type { get; set; }
        public string TypeName { get => Type.ToString(); }
        public string UnitMeas { get; set; } = null!;
        public double? Value { get; set; }
    }
}
