namespace NNDIP.Api.Dtos.Plan
{
    public class AddPlanDto
    {
        public long Id { get; set; }
        public ulong Enabled { get; set; }
        public string Name { get; set; } = null!;
        public long EventId { get; set; }
        public int? Priority { get; set; }
        public string PlanType { get; set; } = null!;
    }
}
