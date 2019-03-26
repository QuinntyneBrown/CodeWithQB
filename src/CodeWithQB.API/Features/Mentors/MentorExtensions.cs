using CodeWithQB.Core.Models;
using System.Linq;
using CodeWithQB.Api.Features.Tags;

namespace CodeWithQB.Api.Features.Mentors
{
    public static class MentorExtensions
    {
        public static MentorDto ToDto (this Mentor x)
            => new MentorDto
            {
                MentorId = x.MentorId,
                FullName = x.FullName,
                Title = x.Title,
                ImageUrl = x.ImageUrl,
                Tags = x.MentorTags.Select(mt => mt.Tag.ToDto()).ToArray()
            };
    }
}
