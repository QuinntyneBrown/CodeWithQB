using CodeWithQB.Core.Exceptions;
using CodeWithQB.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeWithQB.Core.Common
{
    public class CommandPreProcessor : ICommandPreProcessor
    {
        private readonly ICommandRegistry _registry;
        private readonly object sync = new object();

        public CommandPreProcessor(ICommandRegistry registry) => _registry = registry;

        public async Task<TResponse> Process<TRequest, TResponse>(TRequest request, Func<TRequest, Task<TResponse>> asyncHandler) 
            where TRequest : ICommand<TResponse>
        {
            var tcs = new TaskCompletionSource<TResponse>(TaskCreationOptions.RunContinuationsAsynchronously);
            var dependentKeys = default(IEnumerable<string>);
            var partition = default(string);

            try
            {
                lock (sync) dependentKeys = _registry.Register(partition, request.Key, request.SideEffects).GetAwaiter().GetResult();

                if (dependentKeys.Count() > 0)
                {
                    _registry.Subscribe(async (commandRegisteryChanged) =>
                    {
                        if (dependentKeys.Contains($"{commandRegisteryChanged.Partition}-{commandRegisteryChanged.Key}") && !_registry.ContainsAny(dependentKeys).GetAwaiter().GetResult())                        
                            tcs.SetResult(await asyncHandler(request));                        
                    });

                    return await tcs.Task;
                }
                else
                    return await asyncHandler(request);
            }
            catch (Exception e)
            {
                throw new ConcurrencyException();
            }
            finally
            {
                lock (sync) _registry.Clean(partition, request.Key).GetAwaiter().GetResult();
            }
        }        
    }
}
