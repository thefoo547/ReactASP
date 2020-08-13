using App.ErrorHandlers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace WebAPI.Middleware
{
    public class MiddlewareErrorHandler
    {
        private readonly RequestDelegate next;

        public readonly ILogger<MiddlewareErrorHandler> logger;

        public MiddlewareErrorHandler(RequestDelegate next, ILogger<MiddlewareErrorHandler> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext ctx)
        {
            try
            {
                await next(ctx);
            }
            catch (Exception ex)
            {

                await AsyncExceptionHandler(ctx, ex, logger);
            }
        }

        private async Task AsyncExceptionHandler(HttpContext ctx, Exception ex, ILogger<MiddlewareErrorHandler> logger)
        {
            object errors = null;
            switch (ex)
            {
                case BusinessException be:
                    {
                        logger.LogError(ex, "Error Handler");
                        errors = be.Error;
                        ctx.Response.StatusCode = (int)be.StatusCode;
                        break;
                    }
                case Exception e:
                    {
                        logger.LogError(ex, "Error de servidor");
                        errors = string.IsNullOrWhiteSpace(e.Message) ? "Error" : e.Message;
                        break;
                    }
            }
            ctx.Response.ContentType = "application/json";
            if (errors != null)
            {
                var results = JsonConvert.SerializeObject(new { errors });
                await ctx.Response.WriteAsync(results);
            }
        }
    }
}
