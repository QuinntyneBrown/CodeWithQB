using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace CodeWithQB.API.Features.Bookings
{
    public class RemoveBookingCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.BookingId).NotEqual(default(Guid));
            }
        }

        public class Request : IRequest
        {
            public Guid BookingId { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly IEventStore _eventStore;
            
            public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task Handle(Request request, CancellationToken cancellationToken)
            {
                var booking = _eventStore.Load<Booking>(request.BookingId);

                booking.Remove();
                
                _eventStore.Save(booking);

                return Task.CompletedTask;
            }
        }
    }
}
