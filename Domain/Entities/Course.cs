using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Uploaded { get; set; }
        public byte[] FacePhoto { get; set; }
        public Price OfferPrice { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<CourseInstructor> Instructors { get; set; }
    }
}
