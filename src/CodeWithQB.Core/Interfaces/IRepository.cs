using CodeWithQB.Core.Common;
using MediatR;

namespace CodeWithQB.Core.Interfaces
{
    public interface IRepository
    {
        TAggregateRoot[] Query<TAggregateRoot>() where TAggregateRoot : AggregateRoot;

    }
}
