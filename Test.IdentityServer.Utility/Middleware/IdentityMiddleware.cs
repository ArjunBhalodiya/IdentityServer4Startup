using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Test.IdentityServer.Utility.Middleware
{
    public class IdentityMiddleware
    {
        private readonly RequestDelegate _next;

        public IdentityMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {

            return _next(httpContext);
        }
    }
}
