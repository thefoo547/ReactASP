using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Courses
{
    public class QueryAll
    {
        public class CoursesList : IRequest<List<CourseDTO>>
        {

        }

        public class Handler : IRequestHandler<CoursesList, List<CourseDTO>>
        {
            private readonly AppDBContext context;
            private readonly IMapper mapper;
            public Handler(AppDBContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<CourseDTO>> Handle(CoursesList request, CancellationToken cancellationToken)
            {
                var courses = await context.Courses.Include(x=>x.OfferPrice)
                    .Include(x=>x.Comments)
                    .Include(x=>x.Instructors)
                    .ThenInclude(x=>x.Instructor).ToListAsync();

                var coursesDto = mapper.Map<List<Course>, List<CourseDTO>>(courses);

                return coursesDto;
            }
        }
    }
}
