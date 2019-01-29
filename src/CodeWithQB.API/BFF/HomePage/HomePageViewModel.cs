using CodeWithQB.API.Features.Tags;
using System.Collections.Generic;

namespace CodeWithQB.API.BFF.HomePage
{
    public class HomePageViewModel
    {
        public string Title { get; set; }
        public string FullName { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<TagDto> Tags { get; set; }
            = new HashSet<TagDto>();
    }
}
