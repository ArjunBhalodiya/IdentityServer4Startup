using Microsoft.AspNetCore.Mvc;
using Test.IdentityServer.Utility.Attribute;

namespace Test.Identity.Api.Resource.Controllers
{
    [Route("account")]
    [ApiController]
    [IdentityAuthorization("account.api")]
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "This is account API.";
        }
    }
}