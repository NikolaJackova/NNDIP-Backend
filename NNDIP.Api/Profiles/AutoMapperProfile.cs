using AutoMapper;
using NNDIP.Api.Dtos.AddressState;
using NNDIP.Api.Dtos.Data;
using NNDIP.Api.Dtos.Event;
using NNDIP.Api.Dtos.Plan;
using NNDIP.Api.Dtos.Plan.LimitPlan;
using NNDIP.Api.Dtos.Sensor;
using NNDIP.Api.Dtos.YearPeriod;
using NNDIP.Api.Entities;

namespace NNDIP.Api.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddressState, AddressStateDto>();
            CreateMap<Event, EventDto>();
            CreateMap<Data, DataDto>();
            CreateMap<LimitPlan, LimitPlanDto>();
            CreateMap<UpdateLimitPlanDto, LimitPlan>();
            CreateMap<Plan, SimplePlanDto>();
            CreateMap<UpdatePlanDto, Plan>();
            CreateMap<Sensor, SensorDto>();
            CreateMap<YearPeriod, YearPeriodDto>();
            CreateMap<YearPeriod, SimpleYearPeriodDto>();
            CreateMap<UpdateYearPeriodDto, YearPeriod>();
        }
    }
}
