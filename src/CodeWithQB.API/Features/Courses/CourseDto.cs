using CodeWithQB.Core.Models;
using System;

namespace CodeWithQB.Api.Features.Courses
{
    public class CourseDto
    {        
        public Guid CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Abstract { get; set; }
    }

    public static class CourseExtensions
    {        
        public static CourseDto ToDto(this Course course)
            => new CourseDto
            {
                CourseId = course.CourseId,
                Name = course.Name,
                Description = course.Description
            };
    }
}
