using App.Courses;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Course, CourseDTO>()
                .ForMember
                (x => x.Instructors, y => y.MapFrom(z => z.Instructors.Select(a => a.Instructor).ToList()))
                .ForMember(x => x.Comments, y => y.MapFrom(z => z.Comments))
                .ForMember(x => x.Price, y => y.MapFrom(y => y.OfferPrice));
            CreateMap<CourseInstructor, CourseInstructorDTO>();
            CreateMap<Instructor, InstructorDTO>();
            CreateMap<Comment, CommentDTO>();
            CreateMap<Price, PriceDTO>();
        }
    }
}
