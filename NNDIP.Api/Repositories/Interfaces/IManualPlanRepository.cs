using NNDIP.Api.Entities;

namespace NNDIP.Api.Repositories.Interfaces
{
    public interface IManualPlanRepository : IGenericRepository<ManualPlan>
    {
        bool ManualPlanExists(long id);
    }
}
