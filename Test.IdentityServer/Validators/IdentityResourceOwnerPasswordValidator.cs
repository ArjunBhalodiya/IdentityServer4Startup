using System;
using System.Threading.Tasks;
using IdentityServer4.Validation;

namespace Test.IdentityServer.Validators
{
    public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            throw new NotImplementedException();
        }
    }
}
