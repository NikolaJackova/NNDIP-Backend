namespace NNDIP.Api.Entities
{
    public partial class Databasechangeloglock
    {
        public int Id { get; set; }
        public ulong Locked { get; set; }
        public DateTime? Lockgranted { get; set; }
        public string? Lockedby { get; set; }
    }
}
