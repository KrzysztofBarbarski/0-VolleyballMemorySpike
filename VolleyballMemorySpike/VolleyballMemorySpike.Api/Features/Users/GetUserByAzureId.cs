using Microsoft.EntityFrameworkCore;
using VolleyballMemorySpike.Abstraction.CQRS;
using VolleyballMemorySpike.Api.Abstraction;
using VolleyballMemorySpike.Shared.Models;

namespace VolleyballMemorySpike.Api.Features.Users;

public static class GetUserByAzureId 
{
    public sealed record Query(string UserAzureId) : IQuery<User>;

    internal sealed class Handler : IQueryHandler<Query, User>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public Handler(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<Result<User>> Handle(Query request, CancellationToken cancellationToken)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var user = await context.Users
                    .Where(u => string.Equals(u.ObjectIdentifier, request.UserAzureId))
                    .Select(x => new User
                    {
                        CreatedDateUtc = x.CreatedDateUtc,
                        NickName = x.NickName,
                        Id = x.Id,
                        ObjectIdentifier = x.ObjectIdentifier,
                        Email = x.Email

                    })
                    .SingleOrDefaultAsync();

                return user == null ? new User() : user;
            }
        }
    }

}
