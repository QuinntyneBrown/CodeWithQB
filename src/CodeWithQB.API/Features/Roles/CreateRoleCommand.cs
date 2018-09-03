using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace CodeWithQB.API.Features.Roles
{
    public class CreateRoleCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Role.RoleId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public RoleDto Role { get; set; }
        }

        public class Response
        {            
            public Guid RoleId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _eventStore;
            
            public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var role = new Role(request.Role.Name);

                _eventStore.Save(role);
                
                return Task.FromResult(new Response() { RoleId = role.RoleId });
            }
        }
    }
}
