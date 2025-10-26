using MediatR;

namespace WebBFF.Cliente.Application.Interfaces.Base
{
    public interface IDecoradorRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
    }
}
