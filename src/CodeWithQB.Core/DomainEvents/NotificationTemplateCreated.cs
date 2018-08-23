using System;

namespace CodeWithQB.Core.DomainEvents
{
    public class NotificationTemplateCreated: DomainEvent
    {
        public NotificationTemplateCreated(Guid notificationTemplateCreatedId, string name)
        {
            NotificationTemplateId = notificationTemplateCreatedId;
            Name = name;
        }

		public Guid NotificationTemplateId { get; set; }
        public string Name { get; set; }
    }
}
