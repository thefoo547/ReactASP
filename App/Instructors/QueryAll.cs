using MediatR;
using Persistence.DapperConn.Instructor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Instructors
{
    public class QueryAll
    {
        public class Execute : IRequest<List<InstructorModel>>
        {

        }

        public class Handler : IRequestHandler<Execute, List<InstructorModel>>
        {
            private readonly IInstructorRepo instructorRepo;

            public Handler(IInstructorRepo instructorRepo)
            {
                this.instructorRepo = instructorRepo;
            }

            public async Task<List<InstructorModel>> Handle(Execute request, CancellationToken cancellationToken)
            {
                var res = await instructorRepo.FindAll();
                return res.ToList();
            }
        }
    }
}
