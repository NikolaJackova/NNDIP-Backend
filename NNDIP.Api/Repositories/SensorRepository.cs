using NNDIP.Api.Entities;
using NNDIP.Api.NNDIPDbContext;
using NNDIP.Api.Repositories.Interfaces;

namespace NNDIP.Api.Repositories
{
    public class SensorRepository : GenericRepository<Sensor>, ISensorRepository
    {
        public SensorRepository(NndipDbContext context) : base(context)
        {
        }
    }
}
