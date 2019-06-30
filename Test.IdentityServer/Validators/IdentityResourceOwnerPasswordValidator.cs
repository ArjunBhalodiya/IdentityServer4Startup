using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Validation;

namespace Test.IdentityServer.Validators
{
    public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            context.Result = new GrantValidationResult("bb74e8fa-ce2f-49c7-a615-5ccd24b9f4d4",
                                                   OidcConstants.AuthenticationMethods.Password);

            return Task.FromResult(0);
        }
    }
}