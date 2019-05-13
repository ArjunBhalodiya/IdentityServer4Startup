using Microsoft.AspNetCore.Builder;

namespace Test.IdentityServer.Utility.Middleware
{
    public static class IdentityMiddlewareExtensions
    {
        public static IApplicationBuilder UseIdentityMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<IdentityMiddleware>();
        }
    }
}
