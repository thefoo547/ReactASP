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
    public class ListUserRole
    {
        public class Execute : IRequest<List<string>>
        {
            public string Username { get; set; }
        }

        public class ExecuteValidator : AbstractValidator<Execute>
        {
            public ExecuteValidator()
            {
                RuleFor(x => x.Username).NotEmpty();
            }
        }


        public class Handler : IRequestHandler<Execute, List<string>>
        {
            private readonly UserManager<User> userManager;
            private readonly RoleManager<IdentityRole> roleManager;

            public Handler(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
            {
                this.userManager = userManager;
                this.roleManager = roleManager;
            }

            public async Task<List<string>> Handle(Execute request, CancellationToken cancellationToken)
            {
                var user = await userManager.FindByNameAsync(request.Username);

                if (user == null)
                    throw new BusinessException(System.Net.HttpStatusCode.BadRequest,
                        "El usuario no existe");

                return new List<string>(await userManager.GetRolesAsync(user));

            }
        }
    }
}
