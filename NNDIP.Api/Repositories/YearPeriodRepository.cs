using Microsoft.EntityFrameworkCore;
using NNDIP.Api.Entities;
using NNDIP.Api.NNDIPDbContext;
using NNDIP.Api.Repositories.Interfaces;

namespace NNDIP.Api.Repositories
{
    public class YearPeriodRepository : GenericRepository<YearPeriod>, IYearPeriodRepository
    {
        public YearPeriodRepository(NndipDbContext context) : base(context)
        {
        }

        public YearPeriod GetActiveYearPeriod()
        {
            return _context.YearPeriods.Where(result => result.Active == 1).First();
        }

        public async Task<YearPeriod> GetActiveYearPeriodAsync()
        {
            return await _context.YearPeriods.Where(result => result.Active == 1).FirstAsync();
        }

        public override void Update(YearPeriod yearPeriodEntity)
        {
            foreach (var yearPeriodItem in _context.YearPeriods.ToList())
            {
                if (yearPeriodItem.Id != yearPeriodEntity.Id)
                {
                    if (yearPeriodEntity.Active == 1 && yearPeriodItem.Active == 1)
                    {
                        yearPeriodItem.Active = 0;
                        _context.Attach(yearPeriodItem);
                        _context.Entry(yearPeriodItem).State = EntityState.Modified;
                    }
                }
            }
            _context.Attach(yearPeriodEntity);
            _context.Entry(yearPeriodEntity).State = EntityState.Modified;
        }

        public bool YearPeriodExists(long id)
        {
            return _context.YearPeriods.Any(e => e.Id == id);
        }
    }
}
