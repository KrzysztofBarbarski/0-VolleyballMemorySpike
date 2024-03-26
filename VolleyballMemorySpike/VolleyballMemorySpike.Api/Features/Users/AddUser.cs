using Microsoft.EntityFrameworkCore;
using VolleyballMemorySpike.Abstraction.CQRS;
using VolleyballMemorySpike.Api.Abstraction;
using VolleyballMemorySpike.Database.Entities.Users;

namespace VolleyballMemorySpike.Api.Features.Users;

public class AddUser
{
    public sealed record Command(User User) : ICommand;

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
                await context.AddAsync(request.User);
                await context.SaveChangesAsync();

                return Result.Success();
            }
        }
    }
}
