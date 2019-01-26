using CodeWithQB.Core.Models;

namespace CodeWithQB.API.Features.Tags
{
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
