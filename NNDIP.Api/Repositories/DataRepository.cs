using Microsoft.EntityFrameworkCore;
using NNDIP.Api.Dtos.Data;
using NNDIP.Api.Entities;
using NNDIP.Api.NNDIPDbContext;
using NNDIP.Api.Repositories.Interfaces;

namespace NNDIP.Api.Repositories
{
    public class DataRepository : GenericRepository<Data>, IDataRepository
    {
        public DataRepository(NndipDbContext context) : base(context)
        {
        }
        public IEnumerable<Data> GetActualData()
        {
            IQueryable<long> sensorData = _context.Data.Select(data => data.SensorId).Distinct();
            return _context.Data.Include(data => data.Sensor).OrderByDescending(data => data.DataTimestamp).Take(sensorData.Count()).ToList();
        }

        public async Task<IEnumerable<Data>> GetActualDataAsync()
        {
            IQueryable<long> sensorData = _context.Data.Select(data => data.SensorId).Distinct();
            return await _context.Data.Include(data => data.Sensor).OrderByDescending(data => data.DataTimestamp).Take(sensorData.Count()).ToListAsync();
        }

        public IEnumerable<Data> GetActualSensorData(long sensorId)
        {
            IQueryable<long> sensorData = _context.Data.Select(data => data.SensorId).Distinct();
            return _context.Data.Include(data => data.Sensor).OrderByDescending(data => data.DataTimestamp).Take(sensorData.Count()).Where(data => data.SensorId == sensorId).ToList();

        }

        public async Task<IEnumerable<Data>> GetActualSensorDataAsync(long sensorId)
        {
            IQueryable<long> sensorData = _context.Data.Select(data => data.SensorId).Distinct();
            return await _context.Data.Include(data => data.Sensor).OrderByDescending(data => data.DataTimestamp).Take(sensorData.Count()).Where(data => data.SensorId == sensorId).ToListAsync();
        }

        public IEnumerable<Data> GetAllDesc(int? numberOfResults = null)
        {
            if (numberOfResults != null)
            {
                return _context.Data.Include(data => data.Sensor).OrderByDescending(data => data.DataTimestamp).Take(numberOfResults.Value).ToList();
            }
            else
            {
                return _context.Data.Include(data => data.Sensor).OrderByDescending(data => data.DataTimestamp).ToList();
            }
        }

        public async Task<IEnumerable<Data>> GetAllDescAsync(int? numberOfResults = null)
        {
            if (numberOfResults != null)
            {
                return await _context.Data.Include(data => data.Sensor).OrderByDescending(data => data.DataTimestamp).Take(numberOfResults.Value).ToListAsync();
            }
            else
            {
                return await _context.Data.Include(data => data.Sensor).OrderByDescending(data => data.DataTimestamp).ToListAsync();
            }
        }

        public IEnumerable<Data> GetHistoricalData(DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            dateFrom = GetDate(dateFrom, -1);
            dateTo = GetDate(dateTo, 0);
            return _context.Data.Include(data => data.Sensor).Where(data => data.DataTimestamp <= dateTo && data.DataTimestamp >= dateFrom).OrderByDescending(data => data.SensorId).ThenBy(data => data.DataTimestamp).ToList();
        }

        public async Task<IEnumerable<Data>> GetHistoricalDataAsync(DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            dateFrom = GetDate(dateFrom, -1);
            dateTo = GetDate(dateTo, 0);
            return await _context.Data.Include(data => data.Sensor).Where(data => data.DataTimestamp <= dateTo && data.DataTimestamp >= dateFrom).OrderByDescending(data => data.SensorId).ThenBy(data => data.DataTimestamp).ToListAsync();
        }

        public IEnumerable<Data> GetHistoricalSensorData(long sensorId, DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            dateFrom = GetDate(dateFrom, -1);
            dateTo = GetDate(dateTo, 0);
            return _context.Data.Include(data => data.Sensor).Where(data => data.DataTimestamp <= dateTo && data.DataTimestamp >= dateFrom && data.SensorId == sensorId).OrderByDescending(data => data.DataTimestamp).ToList();
        }

        public async Task<IEnumerable<Data>> GetHistoricalSensorDataAsync(long sensorId, DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            dateFrom = GetDate(dateFrom, -1);
            dateTo = GetDate(dateTo, 0);
            return await _context.Data.Include(data => data.Sensor).Where(data => data.DataTimestamp <= dateTo && data.DataTimestamp >= dateFrom && data.SensorId == sensorId).OrderByDescending(data => data.DataTimestamp).ToListAsync();
        }

        private DateTime GetDate(DateTime? dateTime, int addDays)
        {
            DateTime resultDate = dateTime == null ? DateTime.Today.AddDays(addDays) : dateTime.Value;
            return resultDate;
        }
    }
}
