using CodeWithQB.Core.Models;
using System;

namespace CodeWithQB.Api.Features.ContactRequests
{
    public class ContactRequestDto
    {        
        public Guid ContactRequestId { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string Body { get; set; }
        public static ContactRequestDto FromContactRequest(ContactRequest contactRequest)
            => new ContactRequestDto
            {
                ContactRequestId = contactRequest.ContactRequestId,
                Name = contactRequest.Name,
                EmailAddress = contactRequest.EmailAddress,
                Body = contactRequest.Body
            };
    }
}
