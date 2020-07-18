using Domain.Entities;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            public DateTime? Uploaded { get; set; }
            public List<Guid> Instructors { get; set; }
        }
        public class CreateValidate : AbstractValidator<Create>
        {
            public CreateValidate()
            {
                RuleFor(x => x.Title).NotEmpty();
                RuleFor(x => x.Description).NotEmpty();
                RuleFor(x => x.Uploaded).NotEmpty();
            }
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
                    CourseId = Guid.NewGuid(),
                    Title = request.Title,
                    Description = request.Title,
                    Uploaded = request.Uploaded
                };
                context.Courses.Add(course);
                if(request.Instructors != null)
                {
                    foreach (var id in request.Instructors)
                    {
                        var instructorCourse = new CourseInstructor
                        {
                            CourseId = course.CourseId,
                            InstructorId = id
                        };
                        context.CourseInstructors.Add(instructorCourse);
                    }
                }
                var value = await context.SaveChangesAsync();

                return (value > 0) ? Unit.Value : throw new Exception("No se pudo ingresar el curso");
            }
        }
    }
}
