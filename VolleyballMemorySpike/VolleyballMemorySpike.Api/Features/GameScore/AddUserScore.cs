using Microsoft.EntityFrameworkCore;
using VolleyballMemorySpike.Abstraction.CQRS;
using VolleyballMemorySpike.Api.Abstraction;
using VolleyballMemorySpike.Api.Database.Entities;

namespace VolleyballMemorySpike.Features.GameScore;

public static class AddUserScore
{
    public sealed record Command(
        Guid SessionId,
        long Score,
        long UserId) : ICommand;

    internal sealed class Handler : ICommandHandler<Command>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public Handler(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var userScore = new UserScore()
                {
                    UserId = request.UserId,
                    SessionId = request.SessionId,
                    Score = request.Score,
                };

                await context.AddAsync(userScore);
                await context.SaveChangesAsync();

                return Result.Success();
            }
        }
    }
}

