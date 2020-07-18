using App.ErrorHandlers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Courses
{
    public class QueryId
    {
        public class CourseById : IRequest<CourseDTO>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<CourseById, CourseDTO>
        {
            private readonly AppDBContext context;
            private readonly IMapper mapper;
            public Handler(AppDBContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<CourseDTO> Handle(CourseById request, CancellationToken cancellationToken)
            {
                var course = await context.Courses.Include(x => x.OfferPrice)
                    .Include(x => x.Comments)
                    .Include(x => x.Instructors)
                    .ThenInclude(x => x.Instructor).FirstOrDefaultAsync(a => a.CourseId == request.Id);
                if(course == null)
                    throw new BusinessException(HttpStatusCode.NotFound, new { curso = "No se encontró el curso" });

                var courseDto = mapper.Map<Course, CourseDTO>(course);


                return courseDto;
            }
        }
    }
}
