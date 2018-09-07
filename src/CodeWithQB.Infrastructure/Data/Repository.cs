using System.Threading.Tasks;
using CodeWithQB.Core.Common;
using CodeWithQB.Core.DomainEvents;
using CodeWithQB.Core.Interfaces;
using reactive.pipes;

namespace CodeWithQB.Infrastructure.Data
{
    public class Repository : IRepository, IConsume<AggregateChanged>
    {
        public Repository(IEventStore eventStore)
        {
            eventStore.Subscribe(this);
        }

        public async Task<bool> HandleAsync(AggregateChanged message)
        {

            return await Task.FromResult(true);
        }

        public TAggregateRoot[] Query<TAggregateRoot>() where TAggregateRoot : AggregateRoot
        {
            throw new System.NotImplementedException();
        }
    }
}
