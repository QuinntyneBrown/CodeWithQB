using System;

namespace CodeWithQB.Core.Models
{
    public class ContactRequest
    {
        public Guid ContactRequestId { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string Body { get; set; }
    }
}
