using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Test.IdentityServer.Utility.Attribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class IdentityAuthorizationAttribute : ActionFilterAttribute
    {
        private const string scopeClaimType = "scope";

        public string[] Scopes { get; set; } = new string[] { };

        public IdentityAuthorizationAttribute()
        {
            Scopes = null;
        }

        public IdentityAuthorizationAttribute(string scope)
        {
            if (string.IsNullOrEmpty(scope))
            {
                throw new ArgumentNullException("scope");
            }

            Scopes = new[] { scope };
        }

        public IdentityAuthorizationAttribute(string[] scopes)
        {
            Scopes = scopes ?? throw new ArgumentNullException("scopes");
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!ValidateScope(context))
            {
                throw new UnauthorizedAccessException("Invalid Scope!");
            }

            base.OnActionExecuting(context);
        }

        private bool ValidateScope(ActionExecutingContext context)
        {
            HandleUnauthorizedRequest(context);

            if (Scopes == null)
            {
                return true;
            }

            var principal = context.HttpContext.User as ClaimsPrincipal;
            var grantedScopes = principal.FindAll(scopeClaimType).Select(c => c.Value).ToList();

            foreach (var scope in Scopes)
            {
                if (grantedScopes.Contains(scope, StringComparer.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private static void HandleUnauthorizedRequest(ActionExecutingContext context)
        {
            if (context.HttpContext.User.Claims == null || !context.HttpContext.User.Identity.IsAuthenticated)
            {
                throw new UnauthorizedAccessException("Unauthorized!");
            }
        }
    }
}