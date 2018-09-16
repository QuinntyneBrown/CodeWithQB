using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.Core.Interfaces
{
    public interface ICommandRegistry
    {
        Task<IEnumerable<string>> Register(string partition, string key, IEnumerable<string> sideEffects, CancellationToken cancellationToken = default(CancellationToken));

        Task Clean(string partition, string key, CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> ContainsAny(IEnumerable<string> keys, CancellationToken cancellationToken = default(CancellationToken));

        IDisposable Subscribe(Action<ICommandRegistryChanged> onNext);
    }
}
