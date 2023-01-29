﻿namespace NNDIP.Api.Dtos.AddressState
{
    public class AddressStateResultDto
    {
        public bool IsACUnitOn { get; set; }
        public string ACTemperature { get; set; } = null!;
        public string ACUnitMode { get; set; } = null!;
        public string ACUnitFanSpeed { get; set; } = null!;
        public bool IsRecuperationOn { get; set; }

    }
}