using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.Talks
{
    public class UpsertTalkCommand
    {

        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Talk.TalkId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public TalkDto Talk { get; set; }
        }

        public class Response
        {
            public Guid TalkId { get;set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {
                var talk = await _context.Talks.FindAsync(request.Talk.TalkId);

                if (talk == null) {
                    talk = new Talk();
                    _context.Talks.Add(talk);
                }
                
                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { TalkId = talk.TalkId };
            }
        }
    }
}
