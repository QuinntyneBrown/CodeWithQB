using CodeWithQB.Core.Models;
using System;

namespace CodeWithQB.API.Features.Mentees
{
    public class MenteeDto
    {        
        public Guid MenteeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public static MenteeDto FromMentee(Mentee mentee)
            => new MenteeDto
            {
                MenteeId = mentee.MenteeId,
                FirstName = mentee.FirstName,
                LastName = mentee.LastName,
                EmailAddress = mentee.EmailAddress
            };
    }
}
