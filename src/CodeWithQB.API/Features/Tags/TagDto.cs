using System;

namespace CodeWithQB.API.Features.Tags
{
    public class TagDto
    {        
        public Guid TagId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
