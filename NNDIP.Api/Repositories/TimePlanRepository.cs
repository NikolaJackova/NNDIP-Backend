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

        public override void Add(TimePlan timePlan)
        {
            if (timePlan.IdNavigation.PlanType == string.Empty )
            {
                timePlan.IdNavigation.PlanType = EnumExtender.GetEnumDescription(PlanType.TIME_PLAN);
            }
            base.Add(timePlan);
        }

        public override void AddAsync(TimePlan timePlan)
        {
            if (timePlan.IdNavigation.PlanType == string.Empty)
            {
                timePlan.IdNavigation.PlanType = EnumExtender.GetEnumDescription(PlanType.TIME_PLAN);
            }
            base.AddAsync(timePlan);
        }

        public override void Remove(TimePlan timePlan)
        {
            base.Remove(timePlan);
            _context.Plans.Remove(timePlan.IdNavigation);
        }

        public bool TimePlanExists(long id)
        {
            return _context.TimePlans.Any(e => e.Id == id);
        }
    }
}
