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
                (x=>x.Instructors, y=>y.MapFrom(z=>z.Instructors.Select(a=>a.Instructor).ToList()));
            CreateMap<CourseInstructor, CourseInstructorDTO>();
            CreateMap<Instructor, InstructorDTO>();
        }
    }
}
