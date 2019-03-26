using CodeWithQB.Core.Interfaces;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.Api.Features.ContactRequests
{
    public class RemoveContactRequestCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.ContactRequestId).NotNull();
            }
        }

        public class Request: IRequest
        {
            public Guid ContactRequestId { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                var contactRequest = await _context.ContactRequests.FindAsync(request.ContactRequestId);

                _context.ContactRequests.Remove(contactRequest);

                await _context.SaveChangesAsync(cancellationToken);

                return new Unit();
            }
        }
    }
}
