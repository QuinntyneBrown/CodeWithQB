using CodeWithQB.Core.Identity;
using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.Identity
{
    public class AuthenticateCommand
    {
        public class Request: IRequest<Response>
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public class Response
        {
            public string AccessToken { get; set; }
            public Guid UserId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            private readonly ISecurityTokenFactory _securityTokenFactory;

            private readonly IPasswordHasher _passwordHasher;

            public Handler(
                IAppDbContext context, 
                IPasswordHasher passwordHasher,
                ISecurityTokenFactory securityTokenFactory)
            {
                _context = context;
                _passwordHasher = passwordHasher;
                _securityTokenFactory = securityTokenFactory;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var user = await _context.Users
                    .SingleOrDefaultAsync(x => x.Username.ToLower() == request.Username.ToLower());

                if (user == null)
                    throw new Exception("Invalid username or password");

                if (!ValidateUser(user, _passwordHasher.HashPassword(user.Salt, request.Password)))
                    throw new Exception("Invalid username or password");

                
                return new Response()
                {
                    AccessToken = _securityTokenFactory.Create(user.UserId, request.Username),
                    UserId = user.UserId
                };
            }

            public bool ValidateUser(User user, string transformedPassword)
            {
                if (user == null || transformedPassword == null)
                    return false;

                return user.Password == transformedPassword;
            }
        }
    }
}
