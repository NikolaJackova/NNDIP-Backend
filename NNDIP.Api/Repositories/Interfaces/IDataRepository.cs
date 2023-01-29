using NNDIP.Api.Entities;

namespace NNDIP.Api.Repositories.Interfaces
{
    public interface IDataRepository : IGenericRepository<Data>
    {
        IEnumerable<Data> GetAllDesc(int? numberOfResults = null);
        Task<IEnumerable<Data>> GetAllDescAsync(int? numberOfResults = null);

        IEnumerable<Data> GetActualData();
        Task<IEnumerable<Data>> GetActualDataAsync();
        IEnumerable<Data> GetActualSensorData(long sensorId);
        Task<IEnumerable<Data>> GetActualSensorDataAsync(long sensorId);
        IEnumerable<Data> GetHistoricalData(DateTime? dateFrom = null, DateTime? dateTo = null);
        Task<IEnumerable<Data>> GetHistoricalDataAsync(DateTime? dateFrom = null, DateTime? dateTo = null);
        IEnumerable<Data> GetHistoricalSensorData(long sensorId, DateTime? dateFrom = null, DateTime? dateTo = null);
        Task<IEnumerable<Data>> GetHistoricalSensorDataAsync(long sensorId, DateTime? dateFrom = null, DateTime? dateTo = null);
    }
}
