using CodeWithQB.Core.Models;
using System;

namespace CodeWithQB.API.Features.Mentors
{
    public class MentorDto
    {        
        public Guid MentorId { get; set; }
        public string FullName { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }

        public static MentorDto FromMentor(Mentor mentor)
            => new MentorDto
            {
                MentorId = mentor.MentorId,
                FullName = mentor.FullName,
                Title = mentor.Title,
                ImageUrl = mentor.ImageUrl
            };
    }
}
