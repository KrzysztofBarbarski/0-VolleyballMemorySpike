
namespace VolleyballMemorySpike.Client.Services
{
    public interface IGameScoreService
    {
        Task AddUserScoreAsync(Guid sessionId, long score);
    }
}