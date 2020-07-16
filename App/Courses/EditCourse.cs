using App.ErrorHandlers;
using FluentValidation;
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
    public class EditCourse
    {
        public class Update : IRequest
        {
            public int CourseId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime? Uploaded { get; set; }
        }
        public class Handler : IRequestHandler<Update>
        {
            private readonly AppDBContext context;

            public Handler(AppDBContext context)
            {
                this.context = context;
            }
            public class CreateValidate : AbstractValidator<Update>
            {
                public CreateValidate()
                {
                    RuleFor(x => x.Title).NotEmpty();
                    RuleFor(x => x.Description).NotEmpty();
                    RuleFor(x => x.Uploaded).NotEmpty();
                }
            }
            public async Task<Unit> Handle(Update request, CancellationToken cancellationToken)
            {
                var course = await context.Courses.FindAsync(request.CourseId);

                if (course == null)
                    throw new BusinessException(HttpStatusCode.NotFound, new { curso = "No se encontró el curso" });
                    //throw new Exception("Dicho curso no existe");

                course.Title = request.Title ?? course.Title;
                course.Description = request.Description ?? course.Description;
                course.Uploaded = request.Uploaded ?? course.Uploaded;

                var res = await context.SaveChangesAsync();

                return (res > 0) ? Unit.Value : throw new Exception("No se guardaron los cambios");
            }
        }
    }
}
