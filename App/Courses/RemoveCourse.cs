using App.ErrorHandlers;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Courses
{
    public class RemoveCourse
    {
        public class Delete : IRequest
        {
            public int CourseId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime? Uploaded { get; set; }
        }
        public class Handler : IRequestHandler<Delete>
        {
            private readonly AppDBContext context;

            public Handler(AppDBContext context)
            {
                this.context = context;
            }

            public async Task<Unit> Handle(Delete request, CancellationToken cancellationToken)
            {
                var course = await context.Courses.FindAsync(request.CourseId);

                if (course == null)
                    throw new BusinessException(HttpStatusCode.NotFound, new { curso = "No se encotró el curso" });
                    //throw new Exception("Dicho curso no existe");

                context.Remove(course);

                var res = await context.SaveChangesAsync();

                return (res > 0) ? Unit.Value : throw new Exception("No se guardaron los cambios");
            }
        }
    }
}
