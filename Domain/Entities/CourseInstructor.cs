﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class CourseInstructor
    {
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
        public Guid InstructorId { get; set; }
        public Instructor Instructor { get; set; }
    }
}
