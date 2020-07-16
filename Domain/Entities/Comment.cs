using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Student { get; set; }
        public int Puntuation { get; set; }
        public string CommentText { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
