using App.Contracts;
using App.ErrorHandlers;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace App.Secure
{
    public class Login
    {
        public class LoginRequest : IRequest<UserData>
        {
            public string Email { get; set; }
            public string Password { get; set; }

        }

        public class Handler : IRequestHandler<LoginRequest, UserData>
        {
            private readonly UserManager<User> userManager;
            private readonly SignInManager<User> signInManager;
            private readonly IJWTGenerator generator;

            public Handler(UserManager<User> userManager, SignInManager<User> signInManager,
                IJWTGenerator generator)
            {
                this.userManager = userManager;
                this.signInManager = signInManager;
                this.generator = generator;
            }

            public async Task<UserData> Handle(LoginRequest request, CancellationToken cancellationToken)
            {
                var user = await userManager.FindByEmailAsync(request.Email);
                if (user == null)
                    throw new BusinessException(System.Net.HttpStatusCode.NotFound, "No existe el usuario");

                var res = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);

                var roleList = new List<string>(await userManager.GetRolesAsync(user));

                return (res.Succeeded) ? new UserData {
                    FullName = user.FullName,
                    Token = generator.CreateToken(user, roleList),
                    Username = user.UserName,
                    Email = user.Email,
                    Image = null
                } : throw new BusinessException(System.Net.HttpStatusCode.Unauthorized, "Usuario o Contraseña no válidos");
            }
        }

        public class ExecuteValidation : AbstractValidator<LoginRequest>
        {
            public ExecuteValidation()
            {
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }

        
    }
}
