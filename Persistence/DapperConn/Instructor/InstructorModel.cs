﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.DapperConn.Instructor
{
    public class InstructorModel
    {
        public Guid InstructorId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Grade { get; set; }
        public DateTime? Created { get; set; }
    }
}
