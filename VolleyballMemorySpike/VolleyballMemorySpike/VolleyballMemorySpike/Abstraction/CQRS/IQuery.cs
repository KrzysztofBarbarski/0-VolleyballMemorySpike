using MediatR;
using Volleyballn.Api.Abstraction;

namespace VolleyballMemorySpike.Abstraction.CQRS;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
