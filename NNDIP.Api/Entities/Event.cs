namespace NNDIP.Api.Entities
{
    public partial class Event
    {
        public Event()
        {
            Plans = new HashSet<Plan>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Plan> Plans { get; set; }
    }
}
