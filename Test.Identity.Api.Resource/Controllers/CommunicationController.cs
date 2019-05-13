using Microsoft.AspNetCore.Mvc;
using Test.IdentityServer.Utility.Attribute;

namespace Test.Identity.Api.Resource.Controllers
{
    [Route("communication")]
    [ApiController]
    [IdentityAuthorization("communication.api")]
    public class CommunicationController : Controller
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "This is communication API.";
        }
    }
}
