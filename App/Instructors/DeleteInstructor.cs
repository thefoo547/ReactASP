using App.ErrorHandlers;
using FluentValidation;
using MediatR;
using Persistence.DapperConn.Instructor;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace App.Instructors
{
    public class DeleteInstructor
    {
        public class Execute : IRequest
        {
            public Guid InstructorId { get; set; }
        }

        public class ExecuteValidator : AbstractValidator<Execute>
        {
            public ExecuteValidator()
            {
                //RuleFor(x => x.InstructorId).NotEmpty();
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
                return (await instructorRepo.Delete(request.InstructorId) > 0) ?
                    Unit.Value : throw new BusinessException(System.Net.HttpStatusCode.InternalServerError, "No se pudo eliminar");
            }
        }

    }
}
