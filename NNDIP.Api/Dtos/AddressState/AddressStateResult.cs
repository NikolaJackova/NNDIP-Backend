namespace NNDIP.Api.Dtos.AddressState
{
    public class AddressStateResult
    {
        public bool IsACUnitOn { get; set; }
        public bool IsFanOn { get; set; }
        public string ACTemperature { get; set; } = null!;
        public string ACUnitMode { get; set; } = null!;
        public string ACUnitFanSpeed { get; set; } = null!;
        public bool IsRecuperationOn { get; set; }

    }
}
