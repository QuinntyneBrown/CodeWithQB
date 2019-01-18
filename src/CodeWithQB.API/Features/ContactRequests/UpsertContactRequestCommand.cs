using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.ContactRequests
{
    public class UpsertContactRequestCommand
    {

        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.ContactRequest.ContactRequestId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public ContactRequestDto ContactRequest { get; set; }
        }

        public class Response
        {
            public Guid ContactRequestId { get;set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {
                var contactRequest = await _context.ContactRequests.FindAsync(request.ContactRequest.ContactRequestId);

                if (contactRequest == null) {
                    contactRequest = new ContactRequest();
                    _context.ContactRequests.Add(contactRequest);
                }

                contactRequest.Name = request.ContactRequest.Name;

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { ContactRequestId = contactRequest.ContactRequestId };
            }
        }
    }
}
