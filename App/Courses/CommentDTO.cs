using System;
using System.Collections.Generic;
using System.Text;

namespace App.Courses
{
    public class CommentDTO
    {
        public Guid CommentId { get; set; }
        public string Student { get; set; }
        public int Puntuation { get; set; }
        public string CommentText { get; set; }
        public Guid CourseId { get; set; }
        public DateTime? Created { get; set; }
    }
}
