using NNDIP.Api.Entities;

namespace NNDIP.Api.Repositories.Interfaces
{
    public interface ITimePlanRepository : IGenericRepository<TimePlan>
    {
        bool TimePlanExists(long id);
    }
}
