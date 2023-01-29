using NNDIP.Api.Dtos.Plan;

namespace NNDIP.Api.Dtos.Event
{
    public class EventDto
    {
        public EventDto()
        {
            Plans = new HashSet<SimplePlanDto>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<SimplePlanDto> Plans { get; set; }
    }
}
