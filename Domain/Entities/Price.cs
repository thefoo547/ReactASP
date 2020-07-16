using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Price
    {
        public int PriceId { get; set; }
        public decimal ActualPrice { get; set; }
        public decimal Promo { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
