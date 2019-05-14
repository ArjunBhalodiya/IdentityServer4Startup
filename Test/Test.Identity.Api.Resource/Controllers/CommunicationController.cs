using Microsoft.AspNetCore.Mvc;
using Test.IdentityServer.Utility.Attribute;
using Test.IdentityServer.Utility.ControllerExtention;

namespace Test.Identity.Api.Resource.Controllers
{
    [Route("communication")]
    [ApiController]
    [IdentityAuthorization("communication.api")]
    public class CommunicationController : BaseController
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "This is communication API.";
        }
    }
}