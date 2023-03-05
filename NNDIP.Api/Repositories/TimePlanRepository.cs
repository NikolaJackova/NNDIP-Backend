using Microsoft.EntityFrameworkCore;
using NNDIP.Api.Entities;
using NNDIP.Api.Enums;
using NNDIP.Api.NNDIPDbContext;
using NNDIP.Api.Repositories.Interfaces;

namespace NNDIP.Api.Repositories
{
    public class TimePlanRepository : GenericRepository<TimePlan>, ITimePlanRepository
    {
        public TimePlanRepository(NndipDbContext context) : base(context)
        {
        }
        public override IEnumerable<TimePlan> GetAll()
        {
            return _context.TimePlans.Include(timePlan => timePlan.IdNavigation).ToList();
        }

        public override async Task<IEnumerable<TimePlan>> GetAllAsync()
        {
            return await _context.TimePlans.Include(timePlan => timePlan.IdNavigation).ToListAsync();
        }

        public override TimePlan GetById(long id)
        {
            return _context.TimePlans.Include(timePlan => timePlan.IdNavigation).SingleOrDefault(item => item.Id == id);
        }

        public override async Task<TimePlan> GetByIdAsync(long id)
        {
            return await _context.TimePlans.Include(timePlan => timePlan.IdNavigation).SingleOrDefaultAsync(item => item.Id == id);
        }

        public override void Update(TimePlan entity)
        {
            if (string.Empty == entity.IdNavigation.PlanType)
            {
                entity.IdNavigation.PlanType = EnumExtender.GetEnumDescription(Enums.PlanType.TIME_PLAN);
            }
            base.Update(entity);
        }
        public override void Remove(TimePlan entity)
        {
            base.Remove(entity);
            _context.Plans.Remove(entity.IdNavigation);
        }
    }
}
