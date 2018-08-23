using System;

namespace CodeWithQB.Core.DomainEvents
{
    public class NotificationTemplateNameChanged: DomainEvent
    {
        public NotificationTemplateNameChanged( string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
}
