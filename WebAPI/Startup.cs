using App.Contracts;
using App.Courses;
using App.Secure;
using AutoMapper;
using Domain.Entities;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence;
using Persistence.DapperConn;
using Persistence.DapperConn.Instructor;
using Persistence.DapperConn.Pagination;
using Security.Token;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using WebAPI.Middleware;

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
            services.AddCors(o => o.AddPolicy("corsApp", builder =>
              {
                  builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
              }));
            services.AddDbContext<AppDBContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Persistence"));
            });

            services.AddOptions();
            services.Configure<ConnConfig>(Configuration.GetSection("ConnectionStrings"));
            services.AddMediatR(typeof(QueryAll.Handler).Assembly);
            // ES NECESARIO AGREGAR LOS MEDIATR CON ESTRUCTURAS DIFERENTES
            services.AddMediatR(typeof(Login.Handler).Assembly);
            services.AddControllers(opt =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                opt.Filters.Add(new AuthorizeFilter(policy));
            })
                .AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<NewCourse>());
            services.TryAddSingleton<ISystemClock, SystemClock>();

            var authBuilder = services.AddIdentityCore<User>();
            var identityBuilder = new IdentityBuilder(authBuilder.UserType, authBuilder.Services);

            identityBuilder.AddRoles<IdentityRole>();
            identityBuilder.AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<User, IdentityRole>>();


            identityBuilder.AddEntityFrameworkStores<AppDBContext>();
            identityBuilder.AddSignInManager<SignInManager<User>>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt => opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.Unicode.GetBytes("Palabra secreta")),
                    ValidateAudience = false,
                    ValidateIssuer = false
                });

            services.AddScoped<IJWTGenerator, JWTGenerator>();
            services.AddScoped<ISessionUser, SessionUser>();
            services.AddAutoMapper(typeof(QueryAll));

            services.AddTransient<IFactoryConnection, FactoryConnection>();
            services.AddScoped<IInstructorRepo, InstructorRepo>();
            services.AddScoped<IPagination, PaginationRepo>();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Cursos Breves",
                    Version = "v1"
                });
                c.CustomSchemaIds(c => c.FullName);

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("corsApp");
            app.UseMiddleware<MiddlewareErrorHandler>();
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();

            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cursos Breve v1");
            });
            app.UseReDoc(c =>
            {
                c.DocumentTitle = "Breve Courses Docs";
                c.SpecUrl("/swagger/v1/swagger.json");
            });


        }
    }
}
