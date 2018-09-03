using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;

namespace CodeWithQB.API.Features.Roles
{
    public class UpdateRoleCommand
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
                var role = _eventStore.Query<Role>().Single(x => x.RoleId == request.Role.RoleId);

                role.ChangeName(request.Role.Name);

                _eventStore.Save(role);

                return Task.FromResult(new Response() { RoleId = request.Role.RoleId }); 
            }
        }
    }
}
