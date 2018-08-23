using System.Collections.Generic;
using CodeWithQB.Core.Interfaces;

namespace CodeWithQB.Core.Services
{
    public class NotificationBuilder : INotificationBuilder
    {
        private readonly IEventStore _eventStore;

        public NotificationBuilder(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }
        public (List<string> emailToDistributionList, List<string> emailCcDistributionList, string subject, string body) Build(string notificationName)
        {
            throw new System.NotImplementedException();
        }
    }
}
