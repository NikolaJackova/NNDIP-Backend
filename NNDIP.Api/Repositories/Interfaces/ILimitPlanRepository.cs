using NNDIP.Api.Dtos.AddressState;
using NNDIP.Api.Dtos.Plan.LimitPlan;
using NNDIP.Api.Entities;

namespace NNDIP.Api.Repositories.Interfaces
{
    public interface ILimitPlanRepository : IGenericRepository<LimitPlan>
    {
        bool LimitPlanExists(long id);
        IEnumerable<LimitPlan> GetEnabledLimitPlans();
        Task<IEnumerable<LimitPlan>> GetEnabledLimitPlansAsync();
        LimitPlanSettings GetLimitPlanSettings();
        Task<LimitPlanSettings> GetLimitPlanSettingsAsync();
    }
}
