using System;
using System.Collections.Generic;
using System.Text;

namespace App.Courses
{
    public class InstructorDTO
    {
        public Guid InstructorId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Grade { get; set; }
        public byte[] ProfilePhoto { get; set; }
    }
}
