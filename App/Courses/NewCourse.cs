using Domain.Entities;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Courses
{
    public class NewCourse
    {
        public class Create : IRequest
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime Uploaded { get; set; }
        }
        public class Handler : IRequestHandler<Create>
        {
            private readonly AppDBContext context;
            public Handler(AppDBContext context)
            {
                this.context = context;
            }
            public async Task<Unit> Handle(Create request, CancellationToken cancellationToken)
            {
                var course = new Course()
                {
                    Title = request.Title,
                    Description = request.Title,
                    Uploaded = request.Uploaded
                };
                context.Courses.Add(course);
                var value = await context.SaveChangesAsync();

                return (value > 0) ? Unit.Value : throw new Exception("No se pudo ingresar el curso");
            }
        }
    }
}
