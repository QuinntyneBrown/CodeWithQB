using CodeWithQB.Core.Common;
using CodeWithQB.Core.DomainEvents;
using System;

namespace CodeWithQB.Core.Models
{
    public class NotificationTemplate: Entity
    {
        public NotificationTemplate(string name)
            => Apply(new NotificationTemplateCreated(NotificationTemplateId,name));

        public Guid NotificationTemplateId { get; set; } = Guid.NewGuid();          
        public string Name { get; set; }        
        
        protected override void EnsureValidState()
        {
            
        }

        protected override void When(object @event)
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
                    
                    break;
            }
        }

        public void ChangeName(string name)
            => Apply(new NotificationTemplateNameChanged(name));

        public void Remove()
            => Apply(new NotificationTemplateRemoved());
    }
}
