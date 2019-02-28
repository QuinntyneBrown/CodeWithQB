using CodeWithQB.API.Features.Courses;
using CodeWithQB.API.Features.Tags;
using CodeWithQB.API.Features.Videos;
using System.Collections.Generic;

namespace CodeWithQB.API.BFF.HomePage
{
    public class HomePageViewModel
    {
        public string Title { get; set; }
        public string FullName { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<CourseDto> Courses { get; set; }
            = new HashSet<CourseDto>();
        public ICollection<VideoDto> Videos { get; set; }
            = new HashSet<VideoDto>();
        public ICollection<TagDto> Tags { get; set; }
            = new HashSet<TagDto>();
    }
}
