using App.ErrorHandlers;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Persistence;
using Persistence.DapperConn.Instructor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Comments
{
    public class NewComment
    {
        public class Execute : IRequest
        {
            public string Student { get; set; }
            public int Puntuation { get; set; }
            public string CommentText { get; set; }
            public Guid CourseId { get; set; }
        }

        public class ExecuteValidate : AbstractValidator<Execute>
        {
            public ExecuteValidate()
            {
                RuleFor(x => x.Student).NotEmpty();
                RuleFor(x => x.CommentText).NotEmpty();
                RuleFor(x => x.Puntuation).NotEmpty();
                RuleFor(x => x.CourseId).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Execute>
        {
            private readonly AppDBContext appDBContext;

            public Handler(AppDBContext appDBContext)
            {
                this.appDBContext = appDBContext;
            }

            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                var comment = new Comment
                {
                    CommentId = Guid.NewGuid(),
                    Student = request.Student,
                    CommentText = request.CommentText,
                    Puntuation = request.Puntuation,
                    CourseId = request.CourseId
                };

                appDBContext.Comments.Add(comment);

                var res = await appDBContext.SaveChangesAsync();

                return (res > 0) ? Unit.Value :
                    throw new BusinessException(System.Net.HttpStatusCode.InternalServerError, "No se pudo crear");
            }
        }
    }
}
