using System;
using System.Collections.Generic;
using System.Text;

namespace CodeWithQB.Core.Models
{
    public class Mentor
    {
        public Guid MentorId { get; set; }
        public string Title { get; set; }
        public string FullName { get; set; }
        public string ImageUrl { get; set; }
    }
}
