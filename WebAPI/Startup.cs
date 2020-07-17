using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistence;
using App.Courses;
using FluentValidation.AspNetCore;
using WebAPI.Middleware;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Authentication;
using App.Secure;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDBContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b=>b.MigrationsAssembly("Persistence"));
            });
            services.AddMediatR(typeof(QueryAll.Handler).Assembly);
            // ES NECESARIO AGREGAR LOS MEDIATR CON ESTRUCTURAS DIFERENTES
            services.AddMediatR(typeof(Login.Handler).Assembly);
            services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<NewCourse>());
            services.TryAddSingleton<ISystemClock, SystemClock>();

            var authBuilder = services.AddIdentityCore<User>();
            var identityBuilder = new IdentityBuilder(authBuilder.UserType, authBuilder.Services);
            identityBuilder.AddEntityFrameworkStores<AppDBContext>();
            identityBuilder.AddSignInManager<SignInManager<User>>();

        
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<MiddlewareErrorHandler>();
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
