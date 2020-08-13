using App.Contracts;
using App.ErrorHandlers;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.Secure
{
    public class Register
    {
        public class Signup : IRequest<UserData>
        {
            public string Name { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string Username { get; set; }
        }

        public class SignUpValidator : AbstractValidator<Signup>
        {
            public SignUpValidator()
            {
                RuleFor(x => x.Username).NotEmpty();
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.LastName).NotEmpty();
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Signup, UserData>
        {
            private AppDBContext context;
            private UserManager<User> userManager;
            private readonly IJWTGenerator generator;

            public Handler(AppDBContext context, UserManager<User> userManager, IJWTGenerator generator)
            {
                this.context = context;
                this.userManager = userManager;
                this.generator = generator;
            }

            public async Task<UserData> Handle(Signup request, CancellationToken cancellationToken)
            {
                //ver si existe
                var exists = (await context.Users.Where(x => x.Email == request.Email).AnyAsync()
                    || await context.Users.Where(x => x.UserName == request.Username).AnyAsync());
                if (exists)
                    throw new BusinessException(System.Net.HttpStatusCode.BadRequest, new { msg = "Ya existe un usuario registrado con este email" });

                var user = new User
                {
                    FullName = $"{request.Name} {request.LastName}",
                    Email = request.Email,
                    UserName = request.Username
                };

                var res = await userManager.CreateAsync(user, request.Password);

                return (res.Succeeded) ? new UserData
                {
                    FullName = user.FullName,
                    Token = generator.CreateToken(user, null),
                    Username = user.UserName,
                    Email = user.Email
                } : throw new BusinessException(System.Net.HttpStatusCode.InternalServerError, "No se pudo agregar al usuario");

            }
        }

    }
}
