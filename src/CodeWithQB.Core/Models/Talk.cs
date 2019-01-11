using System;

namespace CodeWithQB.Core.Models
{
    public class Talk
    {
        public Guid TalkId { get; set; }
        public DateTime Date { get; set; }
        public string Abstract { get; set; }
        public string Description { get; set; }
        public Guid LocationId { get; set; }
        public Location Location { get; set; }
    }
}
