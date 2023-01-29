using Microsoft.EntityFrameworkCore;
using NNDIP.Api.Dtos.Plan.LimitPlan;
using NNDIP.Api.Entities;
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

        public bool LimitPlanExists(long id)
        {
            return _context.LimitPlans.Any(e => e.Id == id);
        }
    }
}
