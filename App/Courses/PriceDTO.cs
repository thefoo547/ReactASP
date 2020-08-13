using System;

namespace App.Courses
{
    public class PriceDTO
    {
        public Guid PriceId { get; set; }
        public decimal ActualPrice { get; set; }
        public decimal Promo { get; set; }
        public Guid CourseId { get; set; }
    }
}
