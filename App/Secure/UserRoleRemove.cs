using App.ErrorHandlers;
using Domain.Entities;
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
    public class UserRoleRemove
    {
        public class Execute : IRequest
        {
            public string Username { get; set; }
            public string Rolename { get; set; }
        }

        public class ExecuteValidator : AbstractValidator<Execute>
        {
            public ExecuteValidator()
            {
                RuleFor(x => x.Username).NotEmpty();
                RuleFor(x => x.Rolename).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Execute>
        {
            private readonly UserManager<User> userManager;
            private readonly RoleManager<IdentityRole> roleManager;

            public Handler(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
            {
                this.userManager = userManager;
                this.roleManager = roleManager;
            }

            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                var role = await roleManager.FindByNameAsync(request.Rolename);

                if (role == null)
                    throw new BusinessException(System.Net.HttpStatusCode.BadRequest,
                        "El rol no existe");

                var user = await userManager.FindByNameAsync(request.Username);

                if (user == null)
                    throw new BusinessException(System.Net.HttpStatusCode.BadRequest,
                        "El usuario no existe");

                var res = await userManager.RemoveFromRoleAsync(user, role.Name);

                return (res.Succeeded) ? Unit.Value :
                    throw new BusinessException(System.Net.HttpStatusCode.InternalServerError,
                    "No se pudo agregar el rol al usuario");

            }
        }
    }
}
