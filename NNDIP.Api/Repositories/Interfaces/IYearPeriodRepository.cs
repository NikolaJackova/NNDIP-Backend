using NNDIP.Api.Entities;

namespace NNDIP.Api.Repositories.Interfaces
{
    public interface IYearPeriodRepository : IGenericRepository<YearPeriod>
    {
        bool YearPeriodExists(long id);
        YearPeriod GetActiveYearPeriod();
        Task<YearPeriod> GetActiveYearPeriodAsync();
    }
}
