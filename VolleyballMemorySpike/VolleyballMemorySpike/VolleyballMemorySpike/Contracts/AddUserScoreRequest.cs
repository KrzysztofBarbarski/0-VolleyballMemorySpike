namespace VolleyballMemorySpike.Contracts;

public class AddUserScoreRequest
{
    public Guid SessionId { get; set; }
    public long Score { get; set; }
}
