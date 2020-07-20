using App.ErrorHandlers;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Secure
{
    public class NewRole
    {
        public class Execute : IRequest
        {
            public string Name { get; set; }
        }
        public class ValidateExecute : AbstractValidator<Execute>
        {
            public ValidateExecute()
            {
                RuleFor(x => x.Name).NotEmpty();
            }
        }
        public class Handler : IRequestHandler<Execute>
        {
            private readonly RoleManager<IdentityRole> roleManager;

            public Handler(RoleManager<IdentityRole> roleManager)
            {
                this.roleManager = roleManager;
            }

            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                var role = await roleManager.FindByNameAsync(request.Name);
                if(role != null)
                    throw new BusinessException(System.Net.HttpStatusCode.BadRequest, "Ya existe el rol");

                var res = await roleManager.CreateAsync(new IdentityRole(request.Name));

                return (res.Succeeded) ? Unit.Value :
                    throw new BusinessException(System.Net.HttpStatusCode.InternalServerError, "No se guardó");
            }
        }
    }
    
}
