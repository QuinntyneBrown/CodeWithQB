using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CodeWithQB.Core.Models
{
    public class MentorTag
    {
        [ForeignKey("Mentor")]
        public Guid MentorId { get; set; }
        [ForeignKey("Tag")]
        public Guid TagId { get; set; }
        public Mentor Mentor { get; set; }
        public Tag Tag { get; set; }
    }
}
