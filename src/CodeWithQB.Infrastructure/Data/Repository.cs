using CodeWithQB.Core.Common;
using CodeWithQB.Core.DomainEvents;
using CodeWithQB.Core.Interfaces;
using System;

//https://stackoverflow.com/questions/7821404/is-it-possible-to-invoke-subscriberss-onnexts-on-different-threads-in-rx

namespace CodeWithQB.Infrastructure.Data
{
    public class Repository : IRepository, IObserver<AggregateChanged>
    {
        public Repository(IEventStore eventStore)
        {
            eventStore.Subscribe(this);
        }
        
        public void OnCompleted()
        {

        }

        public void OnError(Exception error)
        {

        }

        public void OnNext(AggregateChanged value)
        {
            Console.WriteLine("Works?");
        }

        public TAggregateRoot[] Query<TAggregateRoot>() where TAggregateRoot : AggregateRoot
        {
            throw new System.NotImplementedException();
        }
    }
}
