namespace NNDIP.Api.Entities
{
    public partial class ManualGpioPlan
    {
        public long Id { get; set; }
        public ulong Active { get; set; }

        public virtual GpioPlan IdNavigation { get; set; } = null!;
    }
}
