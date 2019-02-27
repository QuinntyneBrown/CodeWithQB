using CodeWithQB.Core.Models;
using System;

namespace CodeWithQB.API.Features.Courses
{
    public class CourseDto
    {        
        public Guid CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
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
