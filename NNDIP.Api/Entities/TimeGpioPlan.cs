namespace NNDIP.Api.Entities
{
    public partial class TimeGpioPlan
    {
        public long Id { get; set; }
        public int Duration { get; set; }
        public DateTime? LastTriggered { get; set; }

        public virtual GpioPlan IdNavigation { get; set; } = null!;
    }
}
