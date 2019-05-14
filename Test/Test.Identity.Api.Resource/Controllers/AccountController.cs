using Microsoft.AspNetCore.Mvc;
using Test.IdentityServer.Utility.Attribute;
using Test.IdentityServer.Utility.ControllerExtention;

namespace Test.Identity.Api.Resource.Controllers
{
    [Route("account")]
    [ApiController]
    [IdentityAuthorization("account.api")]
    public class AccountController : BaseController
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "This is account API.";
        }
    }
}