using System;

namespace App.Courses
{
    public class InstructorDTO
    {
        public Guid InstructorId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Grade { get; set; }
        public byte[] ProfilePhoto { get; set; }
        public DateTime? Created { get; set; }
    }
}
