using Microsoft.EntityFrameworkCore;
using NNDIP.Api.Dtos.YearPeriod;
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

        public bool YearPeriodExists(long id)
        {
            return _context.YearPeriods.Any(e => e.Id == id);
        }
    }
}
