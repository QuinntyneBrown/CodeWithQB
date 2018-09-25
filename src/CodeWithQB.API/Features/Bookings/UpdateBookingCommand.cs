using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;

namespace CodeWithQB.API.Features.Bookings
{
    public class UpdateBookingCommand
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
				var booking = _eventStore.Load<Booking>(request.Booking.BookingId);
                
                _eventStore.Save(booking);

                return Task.FromResult(new Response() { BookingId = request.Booking.BookingId }); 
            }
        }
    }
}
