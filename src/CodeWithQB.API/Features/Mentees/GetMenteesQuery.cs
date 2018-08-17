using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.Mentees
{
    public class GetMenteesQuery
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public IEnumerable<MenteeDto> Mentees { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _eventStore;

            public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => Task.FromResult(new Response()
                {
                    Mentees = _eventStore.Query<Mentee>().Select(x => MenteeDto.FromMentee(x)).ToList()
                });
        }
    }
}
