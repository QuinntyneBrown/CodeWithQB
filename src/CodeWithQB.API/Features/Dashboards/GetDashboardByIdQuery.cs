using CodeWithQB.API.Features.DashboardCards;
using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.Dashboards
{
    public class GetDashboardByIdQuery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.DashboardId).NotEqual(default(Guid));
            }
        }

        public class Request : IRequest<Response> {
            public Guid DashboardId { get; set; }
        }

        public class Response
        {
            public DashboardDto Dashboard { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IRepository _repository;
            
            public Handler(IRepository repository) => _repository = repository;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var dashboard = _repository.Query<Dashboard>(request.DashboardId);
                var dashboardDto = DashboardDto.FromDashboard(dashboard);
                dashboardDto.DashboardCards = _repository
                    .Query<DashboardCard>(dashboard.DashboardCardIds)
                    .Select(x => DashboardCardDto.FromDashboardCard(x)).ToList();
                
                return Task.FromResult(new Response()
                {
                    Dashboard = dashboardDto
                });

            }
        }
    }
}
