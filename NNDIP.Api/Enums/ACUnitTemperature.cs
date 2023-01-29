using System.ComponentModel;

namespace NNDIP.Api.Enums
{
    public enum ACUnitTemperature
    {
        [Description("15 °C")]
        COOL = 60,
        [Description("25 °C")]
        HEAT = 100,
        [Description("21 °C")]
        DEFAULT = 84,
        [Description("−273,15 °C")]
        NONE = -1
    }
}
