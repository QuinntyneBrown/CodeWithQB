using CodeWithQB.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeWithQB.Core.Common
{
    public class CommandRequestProcessor : ICommandRequestProcessor
    {
        private readonly ICommandRequestRegistry _registry;
        private readonly object sync = new object();

        public CommandRequestProcessor(ICommandRequestRegistry registry) => _registry = registry;

        public async Task<TResponse> Process<TRequest, TResponse>(TRequest request, Func<TRequest, Task<TResponse>> callback) 
            where TRequest : ICommandRequest<TResponse>
        {
            var tcs = new TaskCompletionSource<TResponse>(TaskCreationOptions.RunContinuationsAsynchronously);
            var dependentKeys = default(IEnumerable<string>);

            try
            {
                lock (sync) dependentKeys = _registry.Register(request.Partition, request.Key, request.SideEffects).GetAwaiter().GetResult();

                if (dependentKeys.Count() > 0)
                {
                    _registry.Subscribe(async (commandRegisteryChanged) =>
                    {
                        if (dependentKeys.Contains($"{commandRegisteryChanged.Partition}-{commandRegisteryChanged.Key}") && !_registry.ContainsAny(dependentKeys).GetAwaiter().GetResult())                        
                            tcs.SetResult(await callback(request));                        
                    });

                    return await tcs.Task;
                }
                else
                    return await callback(request);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                lock (sync) _registry.Clean(request.Partition, request.Key).GetAwaiter().GetResult();
            }
        }        
    }
}
