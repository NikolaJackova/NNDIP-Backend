using NNDIP.Api.Entities;
using NNDIP.Api.NNDIPDbContext;
using NNDIP.Api.Repositories.Interfaces;

namespace NNDIP.Api.Repositories
{
    public class ManualPlanRepository : GenericRepository<ManualPlan>, IManualPlanRepository
    {
        public ManualPlanRepository(NndipDbContext context) : base(context)
        {
        }
    }
}
