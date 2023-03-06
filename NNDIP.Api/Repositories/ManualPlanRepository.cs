using Microsoft.EntityFrameworkCore;
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

        public override IEnumerable<ManualPlan> GetAll()
        {
            return _context.ManualPlans.Include(manualPlan => manualPlan.IdNavigation).ToList();
        }

        public override async Task<IEnumerable<ManualPlan>> GetAllAsync()
        {
            return await _context.ManualPlans.Include(manualPlan => manualPlan.IdNavigation).ToListAsync();
        }

        public override ManualPlan GetById(long id)
        {
            return _context.ManualPlans.Include(manualPlan => manualPlan.IdNavigation).SingleOrDefault(item => item.Id == id);
        }

        public override async Task<ManualPlan> GetByIdAsync(long id)
        {
            return await _context.ManualPlans.Include(manualPlan => manualPlan.IdNavigation).SingleOrDefaultAsync(item => item.Id == id);
        }

        public bool ManualPlanExists(long id)
        {
            return _context.ManualPlans.Any(e => e.Id == id);
        }
    }
}
