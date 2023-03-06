using AutoMapper;
using NNDIP.Api.Dtos.AddressState;
using NNDIP.Api.Dtos.Data;
using NNDIP.Api.Dtos.Event;
using NNDIP.Api.Dtos.Plan;
using NNDIP.Api.Dtos.Plan.LimitPlan;
using NNDIP.Api.Dtos.Plan.ManualPlan;
using NNDIP.Api.Dtos.Plan.TimePlan;
using NNDIP.Api.Dtos.Sensor;
using NNDIP.Api.Dtos.YearPeriod;
using NNDIP.Api.Entities;

namespace NNDIP.Api.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddTimePlanDto, TimePlan>();
            CreateMap<AddPlanDto, Plan>();
            CreateMap<AddressState, AddressStateDto>();
            CreateMap<Event, EventDto>();
            CreateMap<Event, SimpleEventDto>();
            CreateMap<Data, DataDto>();
            CreateMap<LimitPlan, LimitPlanDto>();
            CreateMap<TimePlan, TimePlanDto>();
            CreateMap<UpdateLimitPlanDto, LimitPlan>();
            CreateMap<Plan, SimplePlanDto>();
            CreateMap<UpdatePlanDto, Plan>();
            CreateMap<Sensor, SensorDto>();
            CreateMap<YearPeriod, YearPeriodDto>();
            CreateMap<YearPeriod, SimpleYearPeriodDto>();
            CreateMap<SimpleYearPeriodDto, UpdateYearPeriodDto>();
            CreateMap<UpdateYearPeriodDto, YearPeriod>();
            CreateMap<ManualPlan, ManualPlanDto>();
            CreateMap<UpdateManualPlanDto, ManualPlan>();
            CreateMap<AddManualPlanDto, ManualPlan>();
        }
    }
}
