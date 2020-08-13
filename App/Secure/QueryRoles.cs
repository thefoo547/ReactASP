using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace App.Secure
{
    public class QueryRoles
    {
        public class Execute : IRequest<List<IdentityRole>>
        {

        }
        public class Handler : IRequestHandler<Execute, List<IdentityRole>>
        {
            private readonly AppDBContext context;

            public Handler(AppDBContext context)
            {
                this.context = context;
            }

            public async Task<List<IdentityRole>> Handle(Execute request, CancellationToken cancellationToken)
            {
                return await context.Roles.ToListAsync();
            }
        }
    }
}
