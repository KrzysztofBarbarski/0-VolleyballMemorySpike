using VolleyballMemorySpike.Database.Entities.Users;

namespace VolleyballMemorySpike.Database.Entities
{
    // We are store information about each started game
    // SessionId is generated at the same begining, and exist until user will lose.
    public sealed class UserScore
    {
        public Guid SessionId { get; set; }

        public long Score { get; set; }

        public long UserId { get; set; }

        public User User { get; set; }

        public DateTime CreatedDateUtc { get; set; } = DateTime.UtcNow;
    }

    // We are store combined scores of one sessios - (we allow to play multiply times to score more points)
    public sealed class UserScoreCombined
    {
        public Guid SessionId { get; set; }

        public long TotalScore { get; set; }

        public long UserId { get; set; }

        public User User { get; set; }

        public DateTime CreatedDateUtc { get; set; }

        public ICollection<UserScore> UserScores { get; set; } = new List<UserScore>();
    }
}
