using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Comment
    {
        public Guid CommentId { get; set; }
        public string Student { get; set; }
        public int Puntuation { get; set; }
        public string CommentText { get; set; }
        public Guid CourseId { get; set; }
        public DateTime? Created { get; set; }
        public Course Course { get; set; }
    }
}
