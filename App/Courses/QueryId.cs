using App.ErrorHandlers;
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
        public class CourseById : IRequest<Course>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<CourseById, Course>
        {
            private readonly AppDBContext context;
            public Handler(AppDBContext context)
            {
                this.context = context;
            }

            public async Task<Course> Handle(CourseById request, CancellationToken cancellationToken)
            {
                var course = await context.Courses.FindAsync(request.Id);
                if(course == null)
                    throw new BusinessException(HttpStatusCode.NotFound, new { curso = "No se encontró el curso" });

                return course;
            }
        }
    }
}
