using MediatR;

namespace CodeWithQB.Core.Interfaces
{
    public interface IAuthenticatedCommand<TResponse>: IAuthenticatedRequest, IRequest<TResponse>, ICommand<TResponse>
    {
    }
}
