using OAuthFace.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OAuthFace.Filters
{
    public class MyAuthAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (null == filterContext.HttpContext.Session[CConstants.SESSION_ADMNLOGGED])
            {
                filterContext.Result = new RedirectResult("/");
            }
        }
    }
}