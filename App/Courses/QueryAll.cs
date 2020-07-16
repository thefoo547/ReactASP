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
        public class CoursesList : IRequest<List<Course>>
        {

        }

        public class Handler : IRequestHandler<CoursesList, List<Course>>
        {
            private readonly AppDBContext context;
            public Handler(AppDBContext context)
            {
                this.context = context;
            }

            public async Task<List<Course>> Handle(CoursesList request, CancellationToken cancellationToken)
            {
                var courses = await context.Courses.ToListAsync();
                return courses;
            }
        }
    }
}
