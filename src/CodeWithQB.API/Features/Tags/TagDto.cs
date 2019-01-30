using CodeWithQB.Core.Models;
using System;

namespace CodeWithQB.API.Features.Tags
{
    public class TagDto
    {        
        public Guid TagId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public static class TagExtensions
    {
        public static TagDto ToDto(this Tag x) => new TagDto
        {
            TagId = x.TagId,
            Name = x.Name,
            Description = x.Description
        };
    }
}
