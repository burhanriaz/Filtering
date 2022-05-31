using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace Filtering.Custom_Filter
{
    public class AjaxOnlyAttribute : ActionMethodSelectorAttribute
    {
        public override bool IsValidForRequest(RouteContext routeContext, ActionDescriptor action)
        {
            
          //  return routeContext.HttpContext.Request?.Headers["X-Requested-With"] == "XMLHttpRequest";

          var x=  routeContext.HttpContext.Request.Headers["X-Requested-With"];
            if(x== "XMLHttpRequest")
                return true;
            else
                return false;
        }
    }
}
