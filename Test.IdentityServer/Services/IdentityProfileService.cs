using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Services;

namespace Test.IdentityServer.Services
{
    public class IdentityProfileService : IProfileService
    {
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var claims = new List<Claim>
            {
                new Claim("workspace", "sales"),
                new Claim("module", "MyModule1"),
                new Claim("role", "admin"),
                new Claim("username", "adbhalodiya@gmail.com"),
                new Claim("email", "ArjunBhalodiya")
            };

            context.IssuedClaims = claims;
            return Task.FromResult(0);
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
            return Task.FromResult(0);
        }
    }
}
