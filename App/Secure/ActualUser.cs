using App.Contracts;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Secure
{
    public class ActualUser
    {
        public class Execute : IRequest<UserData> { }
        public class Handler : IRequestHandler<Execute, UserData>
        {
            private readonly UserManager<User> userManager;
            private readonly IJWTGenerator generator;
            private readonly ISessionUser sessionUser;

            public Handler(UserManager<User> userManager, IJWTGenerator generator, ISessionUser sessionUser)
            {
                this.userManager = userManager;
                this.generator = generator;
                this.sessionUser = sessionUser;
            }

            public async Task<UserData> Handle(Execute request, CancellationToken cancellationToken)
            {
                var user = await userManager.FindByNameAsync(sessionUser.GetSessionUser());

                return new UserData
                {
                    FullName = user.FullName,
                    Username = user.UserName,
                    Email = user.Email,
                    Token = generator.CreateToken(user),
                    Image = null
                };

            }
        }
    }
}
