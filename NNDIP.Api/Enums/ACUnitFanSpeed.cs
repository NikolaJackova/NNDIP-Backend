using System.ComponentModel;

namespace NNDIP.Api.Enums
{
    public enum ACUnitFanSpeed
    {
        [Description("Quiet")]
        QUIET = 2,
        [Description("Low")]
        LOW = 3,
        [Description("Medium")]
        MED = 4,
        [Description("High")]
        HIGH = 5,
        [Description("None")]
        NONE = -1
    }
}
