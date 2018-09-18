using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.Roles
{
    public class GetRoleByIdQuery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.RoleId).NotEqual(default(Guid));
            }
        }

        public class Request : IRequest<Response> {
            public Guid RoleId { get; set; }
        }

        public class Response
        {
            public RoleDto Role { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IRepository _repository;

            public Handler(IRepository repository) => _repository = repository;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
                 => Task.FromResult(new Response()
                {
                    Role = RoleDto.FromRole(_repository.Query<Role>().Single(x => x.RoleId == request.RoleId))
                });
        }
    }
}
