namespace NNDIP.Api.Entities
{
    public partial class Data
    {
        public long Id { get; set; }
        public int Hits { get; set; }
        public long SensorId { get; set; }
        public DateTime DataTimestamp { get; set; }
        public int? Co2 { get; set; }
        public double? Humidity { get; set; }
        public double? Temperature { get; set; }

        public virtual Sensor Sensor { get; set; } = null!;
    }
}
