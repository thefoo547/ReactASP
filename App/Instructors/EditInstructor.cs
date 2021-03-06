﻿using App.ErrorHandlers;
using FluentValidation;
using MediatR;
using Persistence.DapperConn.Instructor;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace App.Instructors
{
    public class EditInstructor
    {
        public class Execute : IRequest
        {
            public Guid InstructorId { get; set; }
            public string Name { get; set; }
            public string LastName { get; set; }
            public string Grade { get; set; }
        }

        public class ExecuteValidate : AbstractValidator<Execute>
        {
            public ExecuteValidate()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.LastName).NotEmpty();
                RuleFor(x => x.Grade).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Execute>
        {
            private readonly IInstructorRepo instructorRepo;

            public Handler(IInstructorRepo instructorRepo)
            {
                this.instructorRepo = instructorRepo;
            }

            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                return (await instructorRepo.Update(new InstructorModel
                {
                    InstructorId = request.InstructorId,
                    Name = request.Name,
                    LastName = request.LastName,
                    Grade = request.Grade
                }) > 0) ? Unit.Value :
                    throw new BusinessException(System.Net.HttpStatusCode.InternalServerError, "No se pudo ingresar");
            }
        }
    }
}
