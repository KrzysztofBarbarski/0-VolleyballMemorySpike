using Microsoft.EntityFrameworkCore;
using VolleyballMemorySpike.Abstraction.CQRS;
using VolleyballMemorySpike.Api.Abstraction;
using VolleyballMemorySpike.Shared.Models;

namespace VolleyballMemorySpike.Api.Features.UsersRanking;

public static class GetUsersScores
{
    public sealed record Query() : IQuery<List<UserScore>>;

    internal sealed class Handler : IQueryHandler<Query, List<UserScore>>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public Handler(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        //Refactor to Dapper
        public async Task<Result<List<UserScore>>> Handle(Query request, CancellationToken cancellationToken)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var usersScores = await context.UsersScores
                    .OrderByDescending(x => x.Score)
                    .Select(x => new UserScore
                    {
                        Score = x.Score,
                        NickName = x.User.NickName,
                        DateUtc = x.CreatedDateUtc
                    })
                    .ToListAsync();

                return usersScores;
            }
        }
    }
}
