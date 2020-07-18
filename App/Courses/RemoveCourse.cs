using App.ErrorHandlers;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
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
            public Guid CourseId { get; set; }
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
                var instructoresDB = context.CourseInstructors.Where(x => x.CourseId == request.CourseId);

                foreach (var inst in instructoresDB)
                {
                    context.CourseInstructors.Remove(inst);
                }

                var comments = context.Comments.Where(x => x.CourseId == request.CourseId);

                foreach (var comment in comments)
                {
                    context.Comments.Remove(comment);
                }

                var price = context.Prices.Where(x => x.CourseId == request.CourseId).FirstOrDefault();

                if (price != null)
                    context.Prices.Remove(price);

                var course = await context.Courses.FindAsync(request.CourseId);

                if (course == null)
                    throw new BusinessException(HttpStatusCode.NotFound, new { msg = "No se encotró el curso" });
                    //throw new Exception("Dicho curso no existe");

                context.Remove(course);

                var res = await context.SaveChangesAsync();

                return (res > 0) ? Unit.Value : throw new Exception("No se guardaron los cambios");
            }
        }
    }
}
