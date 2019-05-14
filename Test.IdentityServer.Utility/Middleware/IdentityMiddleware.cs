using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Test.IdentityServer.Utility.Constant;
using Test.IdentityServer.Utility.Models;

namespace Test.IdentityServer.Utility.Middleware
{
    public class IdentityMiddleware
    {
        private readonly RequestDelegate _next;

        public IdentityMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var principal = httpContext.User as ClaimsPrincipal;

            if (principal?.Identity != null && principal.Identity.IsAuthenticated)
                await PopulateIdentityUser(httpContext).ConfigureAwait(false);

            await _next(httpContext).ConfigureAwait(false);
            return;
        }

        private async Task PopulateIdentityUser(HttpContext httpContext)
        {
            var principal = httpContext.User as ClaimsPrincipal;
            var userId = principal.FindFirst(GlobalConstants.SubjectClaim).Value;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"{IdentityConfiguration.Services.UserManagement}");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync($"user/{userId}").ConfigureAwait(false);

                try
                {
                    response.EnsureSuccessStatusCode();
                    var user = await response.Content.ReadAsAsync<IdentityUser>().ConfigureAwait(false);
                    httpContext.Items.Add(GlobalConstants.HttpContextActiverUserKey, user);
                    return;
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}