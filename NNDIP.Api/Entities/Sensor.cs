namespace NNDIP.Api.Entities
{
    public partial class Sensor
    {
        public Sensor()
        {
            DashboardSensorConfigs = new HashSet<DashboardSensorConfig>();
            Data = new HashSet<Data>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string SensorType { get; set; } = null!;

        public virtual ICollection<DashboardSensorConfig> DashboardSensorConfigs { get; set; }
        public virtual ICollection<Data> Data { get; set; }
    }
}
