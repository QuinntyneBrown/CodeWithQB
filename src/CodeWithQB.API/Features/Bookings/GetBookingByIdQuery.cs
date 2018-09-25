using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.Bookings
{
    public class GetBookingByIdQuery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.BookingId).NotEqual(default(Guid));
            }
        }

        public class Request : IRequest<Response> {
            public Guid BookingId { get; set; }
        }

        public class Response
        {
            public BookingDto Booking { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IRepository _repository;
            
			public Handler(IRepository repository) => _repository = repository;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
			     => Task.FromResult(new Response()
                {
                    Booking = BookingDto.FromBooking(_repository.Query<Booking>().Single(x => x.BookingId == request.BookingId))
                });
        }
    }
}
