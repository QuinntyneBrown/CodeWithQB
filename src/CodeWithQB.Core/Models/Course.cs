using System;

namespace CodeWithQB.Core.Models
{
    public class Course
    {
        public Guid CourseId { get; set; }
		public string Name { get; set; }
        public string Description { get; set; }
        public string Abstract { get; set; }
    }
}
