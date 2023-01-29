using Microsoft.EntityFrameworkCore;

namespace NNDIP.Api.Repositories.Interfaces
{
    public interface IRepositoryWrapper
    {
        IYearPeriodRepository YearPeriodRepository { get; }

        ISensorRepository SensorRepository { get; }

        IDataRepository DataRepository { get; }

        IUserRepository UserRepository { get; }

        ILimitPlanRepository LimitPlanRepository { get; }

        IAddressStateRepository AddressStateRepository { get; }
        Task SaveAsync();
        void Save();
    }
}
