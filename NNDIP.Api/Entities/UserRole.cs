namespace NNDIP.Api.Entities
{
    public partial class UserRole
    {
        public long UserId { get; set; }
        public int? Roles { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
