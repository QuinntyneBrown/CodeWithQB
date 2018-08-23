using CodeWithQB.Core.Common;
using CodeWithQB.Core.DomainEvents;
using System;

namespace CodeWithQB.Core.Models
{
    public class NotificationTemplate: AggregateRoot
    {
        public NotificationTemplate(string name)
            => Apply(new NotificationTemplateCreated(NotificationTemplateId,name));

        public Guid NotificationTemplateId { get; set; } = Guid.NewGuid();          
		public string Name { get; set; }        
		public bool IsDeleted { get; set; }

        protected override void EnsureValidState()
        {
            
        }

        protected override void When(DomainEvent @event)
        {
            switch (@event)
            {
                case NotificationTemplateCreated notificationTemplateCreated:
                    Name = notificationTemplateCreated.Name;
					NotificationTemplateId = notificationTemplateCreated.NotificationTemplateId;
                    break;

                case NotificationTemplateNameChanged notificationTemplateNameChanged:
                    Name = notificationTemplateNameChanged.Name;
                    break;

                case NotificationTemplateRemoved notificationTemplateRemoved:
                    IsDeleted = true;
                    break;
            }
        }

        public void ChangeName(string name)
            => Apply(new NotificationTemplateNameChanged(name));

        public void Remove()
            => Apply(new NotificationTemplateRemoved());
    }
}
