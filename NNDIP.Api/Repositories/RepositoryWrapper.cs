using NNDIP.Api.NNDIPDbContext;
using NNDIP.Api.Repositories.Interfaces;

namespace NNDIP.Api.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private IYearPeriodRepository _yearPeriod;
        public IYearPeriodRepository YearPeriodRepository
        {
            get
            {
                _yearPeriod ??= new YearPeriodRepository(_context);
                return _yearPeriod;
            }
        }

        private ISensorRepository _sensor;
        public ISensorRepository SensorRepository
        {
            get
            {
                _sensor ??= new SensorRepository(_context);
                return _sensor;
            }
        }

        private IDataRepository _data;
        public IDataRepository DataRepository
        {
            get
            {
                _data ??= new DataRepository(_context);
                return _data;
            }
        }

        private IUserRepository _user;
        public IUserRepository UserRepository
        {
            get
            {
                _user ??= new UserRepository(_context);
                return _user;
            }
        }

        private ILimitPlanRepository _limitPlan;
        public ILimitPlanRepository LimitPlanRepository
        {
            get
            {
                _limitPlan ??= new LimitPlanRepository(_context);
                return _limitPlan;
            }
        }

        private IAddressStateRepository _addressStateRepository;
        public IAddressStateRepository AddressStateRepository
        {
            get
            {
                _addressStateRepository ??= new AddressStateRepository(_context);
                return _addressStateRepository;
            }
        }

        protected readonly NndipDbContext _context;
        public RepositoryWrapper(NndipDbContext context)
        {
            _context = context;
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
