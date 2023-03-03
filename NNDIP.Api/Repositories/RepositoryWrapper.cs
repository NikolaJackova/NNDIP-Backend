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

        private IEventRepository _eventRepository;
        public IEventRepository EventRepository
        {
            get
            {
                _eventRepository ??= new EventRepository(_context);
                return _eventRepository;
            }
        }


        private IPlanRepository _planRepository;
        public IPlanRepository PlanRepository
        {
            get
            {
                _planRepository ??= new PlanRepository(_context);
                return _planRepository;
            }
        }

        private ITimePlanRepository _timePlanRepository;
        public ITimePlanRepository TimePlanRepository
        {
            get
            {
                _timePlanRepository ??= new TimePlanRepository(_context);
                return _timePlanRepository;
            }
        }

        private IManualPlanRepository _manualPlanRepository;
        public IManualPlanRepository ManualPlanRepository
        {
            get
            {
                _manualPlanRepository ??= new ManualPlanRepository(_context);
                return _manualPlanRepository;
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
