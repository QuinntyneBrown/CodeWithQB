using CodeWithQB.Core.Models;
using System;

namespace CodeWithQB.API.Features.Videos
{
    public class VideoDto
    {        
        public Guid VideoId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
    }

    public static class VideoExtensions
    {        
        public static VideoDto ToDto(this Video video)
            => new VideoDto
            {
                VideoId = video.VideoId,
                Name = video.Name,
                Description = video.Description,
                Url = video.Url
            };
    }
}
