namespace NNDIP.Api.Entities
{
    public partial class User
    {
        public long Id { get; set; }
        public string? HashedPassword { get; set; }
        public string? Name { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string? Username { get; set; }
        public int? Age { get; set; }
    }
}
