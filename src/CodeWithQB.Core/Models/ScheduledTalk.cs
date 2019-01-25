using NodaTime;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeWithQB.Core.Models
{
    public class ScheduledTalk
    {
        public int ScheduledTalkId { get; set; }

        public LocalDate Date { get; set; }

        public LocalTime Time { get; set; }

        [ForeignKey("Talk")]
        public Guid TalkId { get; set; }

        public Guid LocationId { get; set; }

        public Location Location { get; set; }

        public Talk Talks { get; set; }

    }
}
