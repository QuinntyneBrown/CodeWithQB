using System.Collections.Generic;

namespace CodeWithQB.Core.Interfaces
{
    public interface INotificationBuilder
    {
        (List<string> emailToDistributionList, List<string> emailCcDistributionList, string subject, string body) Build(string notificationName);
    }
}
