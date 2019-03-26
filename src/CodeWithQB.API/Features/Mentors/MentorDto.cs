using CodeWithQB.Api.Features.Tags;
using CodeWithQB.Core.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CodeWithQB.Api.Features.Mentors
{
    public class MentorDto
    {        
        public Guid MentorId { get; set; }
        public string FullName { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }

        public ICollection<TagDto> Tags { get; set; }
            = new HashSet<TagDto>();        
    }
}
