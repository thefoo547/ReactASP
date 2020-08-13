using MediatR;
using Persistence.DapperConn.Instructor;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace App.Instructors
{
    public class QueryId
    {
        public class Execute : IRequest<InstructorModel>
        {
            public Guid InstructorId { get; set; }
        }

        public class Handler : IRequestHandler<Execute, InstructorModel>
        {
            private readonly IInstructorRepo instructorRepo;

            public Handler(IInstructorRepo instructorRepo)
            {
                this.instructorRepo = instructorRepo;
            }

            public async Task<InstructorModel> Handle(Execute request, CancellationToken cancellationToken)
            {
                var res = await instructorRepo.GetById(request.InstructorId);
                return res;
            }
        }
    }
}
