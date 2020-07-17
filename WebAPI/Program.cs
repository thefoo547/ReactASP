using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistence;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hostserver= CreateHostBuilder(args).Build();
            using(var env = hostserver.Services.CreateScope())
            {
                var services = env.ServiceProvider;
                try
                {
                    var usrmgr = services.GetRequiredService<UserManager<User>>();
                    var ctx = services.GetRequiredService<AppDBContext>();
                    ctx.Database.Migrate();
                    DataPrueba7.InsertarData(ctx, usrmgr).Wait();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            hostserver.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
