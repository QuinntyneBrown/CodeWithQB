using System;
using System.Threading.Tasks;

namespace CodeWithQB.Core.Interfaces
{
    public interface ICommandRequestProcessor
    {        
        Task<TResponse> Process<TRequest,TResponse>(TRequest request, Func<TRequest, Task<TResponse>> callback) 
            where TRequest : ICommandRequest<TResponse>;
    }
}
