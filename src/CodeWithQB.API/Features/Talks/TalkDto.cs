using CodeWithQB.Core.Models;
using System;

namespace CodeWithQB.API.Features.Talks
{
    public class TalkDto
    {        
        public Guid TalkId { get; set; }
        public string Name { get; set; }

        public static TalkDto FromTalk(Talk talk)
            => new TalkDto
            {
                TalkId = talk.TalkId
            };
    }
}
