namespace NNDIP.Api.Entities
{
    public partial class YearPeriod
    {
        public YearPeriod()
        {
            LimitPlans = new HashSet<LimitPlan>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public ulong Active { get; set; }

        public virtual ICollection<LimitPlan> LimitPlans { get; set; }
    }
}
