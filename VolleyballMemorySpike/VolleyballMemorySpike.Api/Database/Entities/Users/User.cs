using System.ComponentModel.DataAnnotations;

namespace VolleyballMemorySpike.Database.Entities.Users
{
    public sealed class User
    {
        public long Id { get; set; }

        public string ObjectIdentifier { get; set; } = string.Empty;

        [Required]
        public string NickName { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        public DateTime CreatedDateUtc { get; set; } = DateTime.UtcNow;
    }
}
