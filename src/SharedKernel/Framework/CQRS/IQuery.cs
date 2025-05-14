using MediatR;

namespace Framework.CQRS;

public interface IQuery<out TResponse> : IRequest<TResponse>
    where TResponse: notnull
{
}
