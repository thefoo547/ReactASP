using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class DataPrueba7
    {
        public static async Task InsertarData(AppDBContext context, UserManager<User> manager)
        {
#pragma warning disable EF1001 // Internal EF Core API usage.
            if (!manager.Users.Any())
#pragma warning restore EF1001 // Internal EF Core API usage.
            {
                var user = new User { FullName = "Pepito Perez", UserName = "peperez", Email = "peperez@uwu.com" };
                await manager.CreateAsync(user, "Soyadmin.123");
            }
        }
    }
}
