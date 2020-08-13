using App.Contracts;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace Security.Token
{
    public class SessionUser : ISessionUser
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public SessionUser(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string GetSessionUser()
        {
            return httpContextAccessor.HttpContext.User?.Claims?
                .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
