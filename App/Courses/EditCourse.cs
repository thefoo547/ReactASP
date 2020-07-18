using App.ErrorHandlers;
using Domain.Entities;
using FluentValidation;
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
    public class EditCourse
    {
        public class Update : IRequest
        {
            public Guid CourseId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime? Uploaded { get; set; }
            public List<Guid> Instructors { get; set; }
            public decimal? PriceActual { get; set; }
            public decimal? Discount { get; set; }

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

                var pricen = context.Prices.Where(x => x.CourseId == course.CourseId).FirstOrDefault();
                if(pricen != null)
                {
                    pricen.Promo = request.Discount ?? pricen.Promo;
                    pricen.ActualPrice = request.PriceActual ?? pricen.ActualPrice;
                }
                else
                {
                    pricen = new Price
                    {
                        PriceId = Guid.NewGuid(),
                        ActualPrice = request.PriceActual ?? 0,
                        Promo = request.Discount ?? 0,
                        CourseId = course.CourseId
                    };
                }

                if(request.Instructors != null && request.Instructors.Count > 0)
                {
                    var instructorsDB = context.CourseInstructors.Where(x => x.CourseId == request.CourseId).ToList();
                    foreach (var ins in instructorsDB)
                    {
                        context.CourseInstructors.Remove(ins);
                    }
                    foreach(var ins in request.Instructors)
                    {
                        var newins = new CourseInstructor
                        {
                            CourseId = request.CourseId,
                            InstructorId = ins
                        };
                        context.CourseInstructors.Add(newins);
                    }
                }

                var res = await context.SaveChangesAsync();

                return (res > 0) ? Unit.Value : throw new Exception("No se guardaron los cambios");
            }
        }
    }
}
