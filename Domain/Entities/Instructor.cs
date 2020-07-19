using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Instructor
    {
        public Guid InstructorId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Grade { get; set; }
        public byte[] ProfilePhoto { get; set; }
        public DateTime? Created { get; set; }
        public ICollection<CourseInstructor> Courses { get; set; }
    }
}
