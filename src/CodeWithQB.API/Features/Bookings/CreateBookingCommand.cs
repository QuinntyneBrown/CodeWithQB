using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace CodeWithQB.API.Features.Bookings
{
    public class CreateBookingCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Booking.BookingId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public BookingDto Booking { get; set; }
        }

        public class Response
        {			
            public Guid BookingId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _eventStore;
            
			public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var booking = new Booking("");

                _eventStore.Save(booking);
                
                return Task.FromResult(new Response() { BookingId = booking.BookingId });
            }
        }
    }
}
