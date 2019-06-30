using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Test.IdentityServer.Utility.Constant;
using Test.IdentityServer.Utility.Models;

namespace Test.IdentityServer.Utility.ControllerExtention
{
    public class BaseController : Controller
    {
        public IdentityUser ActiveUser { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var principal = context.HttpContext.User as ClaimsPrincipal;

            if (principal?.Identity != null && principal.Identity.IsAuthenticated)
            {
                ActiveUser = Task.FromResult(PopulateIdentityUser(context.HttpContext).Result).Result;
            }

            base.OnActionExecuting(context);
        }

        private static async Task<IdentityUser> PopulateIdentityUser(HttpContext httpContext)
        {
            var principal = httpContext.User as ClaimsPrincipal;
            var userId = principal.FindFirst(GlobalConstants.SubjectClaim).Value;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"{IdentityConfiguration.Services.UserManagement}");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync($"user/{userId}").ConfigureAwait(false);

                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<IdentityUser>().ConfigureAwait(false);
            }
        }
    }
}
