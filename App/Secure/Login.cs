﻿using App.ErrorHandlers;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
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

            public Handler(UserManager<User> userManager, SignInManager<User> signInManager)
            {
                this.userManager = userManager;
                this.signInManager = signInManager;
            }

            public async Task<UserData> Handle(LoginRequest request, CancellationToken cancellationToken)
            {
                var user = await userManager.FindByEmailAsync(request.Email);
                if (user == null)
                    throw new BusinessException(System.Net.HttpStatusCode.NotFound, "No existe el usuario");

                var res = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);

                return (res.Succeeded) ? new UserData { 
                    FullName = user.FullName,
                    Token = "DATA DEL TOKE MI REY",
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
