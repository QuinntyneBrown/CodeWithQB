using CodeWithQB.Core.DomainEvents;
using CodeWithQB.Core.Identity;
using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using MediatR;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.API.ProcessManagers
{
    public class MenteeRegistrationProcess
        :INotificationHandler<MenteeRegistrationRequested>
    {
        private readonly IEventStore _eventStore;
        private readonly IMediator _mediator;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IRepository _repository;
        public MenteeRegistrationProcess(
            IEventStore eventStore, 
            IMediator mediator, 
            IPasswordHasher passwordHasher,
            IRepository repository
            )
        {
            _eventStore = eventStore;
            _mediator = mediator;
            _passwordHasher = passwordHasher;
        }

        public async Task Handle(MenteeRegistrationRequested notification, CancellationToken cancellationToken)
        {
            var user = _repository.Query<User>().Single(x => x.Username == notification.EmailAddress);

            var salt = new byte[128 / 8];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            if (user == null)
                user = new User(notification.EmailAddress, salt, _passwordHasher.HashPassword(salt, notification.Password));

            var role = _repository.Query<Role>().Single(x => x.Name == "Mentee");

            user.AddRole(role.RoleId);

            _eventStore.Save(user);

            var dashboard = new Dashboard("Default", user.UserId);

            _eventStore.Save(dashboard);

            await Task.CompletedTask;
        }        
    }
}
