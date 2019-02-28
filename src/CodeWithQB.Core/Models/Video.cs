using System;

namespace CodeWithQB.Core.Models
{
    public class Video
    {
        public Guid VideoId { get; set; }
		public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Abstract { get; set; }
    }
}
