namespace NNDIP.Api.Entities
{
    public partial class EventAction
    {
        public long EventId { get; set; }
        public long ActionId { get; set; }

        public virtual Action Action { get; set; } = null!;
        public virtual Event Event { get; set; } = null!;
    }
}
