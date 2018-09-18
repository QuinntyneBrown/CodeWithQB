using CodeWithQB.Core.Exceptions;
using CodeWithQB.Core.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.Core.Common
{
    public class CommandRegistry : ICommandRegistry
    {
        private readonly ConcurrentDictionary<string, IEnumerable<string>> _inner = new ConcurrentDictionary<string, IEnumerable<string>>();
        private readonly Subject<ICommandRegistryChanged> _subject = new Subject<ICommandRegistryChanged>();

        public async Task Clean(string partition, string key, CancellationToken cancellationToken = default(CancellationToken))
        {
            _inner.Remove($"{partition}-{key}", out IEnumerable<string> _);

            _subject.OnNext(new CommandRegistryChanged(partition, key));

            await Task.CompletedTask;
        }

        public async Task<bool> ContainsAny(IEnumerable<string> keys, CancellationToken cancellationToken = default(CancellationToken)) => await Task.FromResult(keys.Any(x => _inner.ContainsKey(x)));
        
        public async Task<IEnumerable<string>> Register(string partition, string key, IEnumerable<string> sideEffects, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (_inner.ContainsKey($"{partition}-{key}"))
                throw new ConcurrencyException();

            _inner.TryAdd($"{partition}-{key}", sideEffects);

            return await GetKeys(partition, key, sideEffects);
        }

        public IDisposable Subscribe(Action<ICommandRegistryChanged> onNext) => _subject.Subscribe(onNext);

        private async Task<IEnumerable<string>> GetKeys(string partition, string key, IEnumerable<string> sideEffects)
        {
            var keys = new List<string>();

            foreach (var item in _inner.Where(x => x.Key.StartsWith($"{partition}") && x.Key != $"{partition}-{key}"))
                foreach (var sideEffect in sideEffects)
                    if (item.Value.Any(x => x == sideEffect))
                        keys.Add(item.Key);

            return await Task.FromResult(keys);
        }
    }
}
