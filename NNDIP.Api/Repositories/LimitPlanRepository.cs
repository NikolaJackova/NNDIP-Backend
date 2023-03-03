using Microsoft.EntityFrameworkCore;
using NNDIP.Api.Dtos.Plan.LimitPlan;
using NNDIP.Api.Entities;
using NNDIP.Api.Enums;
using NNDIP.Api.NNDIPDbContext;
using NNDIP.Api.Repositories.Interfaces;

namespace NNDIP.Api.Repositories
{
    public class LimitPlanRepository : GenericRepository<LimitPlan>, ILimitPlanRepository
    {
        public LimitPlanRepository(NndipDbContext context) : base(context)
        {
        }

        public IEnumerable<LimitPlan> GetEnabledLimitPlans()
        {
            return _context.LimitPlans.Include(limitPlan => limitPlan.IdNavigation).Where(result => result.IdNavigation.Enabled == 1);
        }

        public async Task<IEnumerable<LimitPlan>> GetEnabledLimitPlansAsync()
        {
            return await _context.LimitPlans.Include(limitPlan => limitPlan.IdNavigation).Where(result => result.IdNavigation.Enabled == 1).ToListAsync();
        }

        public LimitPlanSettings GetLimitPlanSettings()
        {
            YearPeriod activeYearPeriod = _context.YearPeriods.Where(result => result.Active == 1).First();
            List<LimitPlan> limitPlans = _context.LimitPlans.Where(item => item.YearPeriodId == activeYearPeriod.Id).ToList();
            return GetLimitPlanSettings(activeYearPeriod, limitPlans);
        }

        public async Task<LimitPlanSettings> GetLimitPlanSettingsAsync()
        {
            YearPeriod activeYearPeriod = await _context.YearPeriods.Where(result => result.Active == 1).FirstAsync();
            List<LimitPlan> limitPlans = await _context.LimitPlans.Where(item => item.YearPeriodId == activeYearPeriod.Id).ToListAsync();
            return GetLimitPlanSettings(activeYearPeriod, limitPlans);
        }

        private LimitPlanSettings GetLimitPlanSettings(YearPeriod activeYearPeriod, List<LimitPlan> limitPlans)
        {
            LimitPlanSettings limitPlanSettings = new LimitPlanSettings()
            {
                YearPeriodDto = new Dtos.YearPeriod.SimpleYearPeriodDto()
                {
                    Id = activeYearPeriod.Id,
                    Name = activeYearPeriod.Name,
                    Active = activeYearPeriod.Active
                }
            };

            LimitPlan? limitPlanTemperature = limitPlans.FirstOrDefault(item => item.ValueType == EnumExtender.GetEnumDescription(Enums.ValueType.TEMPERATURE_HIGH) || item.ValueType == EnumExtender.GetEnumDescription(Enums.ValueType.TEMPERATURE_LOW));
            if (limitPlanTemperature is not null)
            {
                limitPlanSettings.OptimalValueTemperature = limitPlanTemperature.OptimalValue;
            }

            LimitPlan? limitPlanCo2 = limitPlans.FirstOrDefault(item => item.ValueType == EnumExtender.GetEnumDescription(Enums.ValueType.CO2));
            if (limitPlanCo2 is not null)
            {
                limitPlanSettings.OptimalValueCo2 = limitPlanCo2.OptimalValue;
            }

            foreach (var limitPlan in limitPlans)
            {
                if (limitPlan.ValueType == EnumExtender.GetEnumDescription(Enums.ValueType.TEMPERATURE_LOW))
                {
                    limitPlanSettings.TemperatureLow = GetThreshold(limitPlan);
                }
                else if (limitPlan.ValueType == EnumExtender.GetEnumDescription(Enums.ValueType.TEMPERATURE_HIGH))
                {
                    limitPlanSettings.TemperatureHigh = GetThreshold(limitPlan);
                }
                else if (limitPlan.ValueType == EnumExtender.GetEnumDescription(Enums.ValueType.CO2))
                {
                    limitPlanSettings.Co2 = GetThreshold(limitPlan);
                }
            }
            return limitPlanSettings;
        }

        public bool LimitPlanExists(long id)
        {
            return _context.LimitPlans.Any(e => e.Id == id);
        }

        private Threshold GetThreshold(LimitPlan limitPlan)
        {
            Threshold threshold = new()
            {
                Id = limitPlan.Id,
                Value = limitPlan.ThresholdValue
            };
            Plan? plan = _context.Plans.Find(limitPlan.Id);
            if (plan is not null)
            {
                threshold.Enabled = plan.Enabled;
                threshold.EventId = plan.EventId;
            }
            return threshold;
        }
    }
}
