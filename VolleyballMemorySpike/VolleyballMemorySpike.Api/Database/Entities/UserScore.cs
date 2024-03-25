using VolleyballMemorySpike.Database.Entities.Users;

namespace VolleyballMemorySpike.Api.Database.Entities
{
    public sealed class UserScore
    {
        public Guid SessionId { get; set; }

        public long Score { get; set; }

        public long UserId { get; set; }

        public User? User { get; set; }

        public DateTime CreatedDateUtc { get; set; } = DateTime.UtcNow;
    }
}
