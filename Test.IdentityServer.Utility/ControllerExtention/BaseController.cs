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
            context.HttpContext.Items.TryGetValue(GlobalConstants.HttpContextActiverUserKey, out object userObject);
            if (userObject != null)
                ActiveUser = (IdentityUser)userObject;

            base.OnActionExecuting(context);
        }
    }
}
