using NodaTime;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeWithQB.Core.Models
{
    public class ScheduledMentoringSession
    {
        public Guid ScheduledMentoringSessionId { get; set; }

        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }

        [ForeignKey("Mentor")]
        public Guid MentorId { get; set; }

        public IsoDayOfWeek DayOfWeek { get; set; }

        public LocalTime LocalStartTime { get; set; }

        public LocalTime LocalEndTime { get; set; }
    }
}
