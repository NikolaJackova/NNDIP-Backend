using NNDIP.Api.Dtos.YearPeriod;
using NNDIP.Api.Enums;

namespace NNDIP.Api.Dtos.Plan.LimitPlan
{
    public class LimitPlanSettings
    {
        public SimpleYearPeriodDto YearPeriodDto { get; set; } = null!;
        public double OptimalValueTemperature { get; set; }
        public double OptimalValueCo2 { get; set; }
        public Threshold TemperatureLow { get; set; } = null!;
        public Threshold TemperatureHigh { get; set; } = null!;
        public Threshold Co2 { get; set; } = null!;
        public bool IsWinterActive
        {
            get
            {
                return YearPeriodDto.Name == EnumExtender.GetEnumDescription(YearPeriodType.WINTER);
            }
        }

        public bool IsSummerActive
        {
            get
            {
                return YearPeriodDto.Name == EnumExtender.GetEnumDescription(YearPeriodType.SUMMER);
            }
        }
    }
}
