using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TuVanLaptoop.Filter
{
    public class MessageFilter: ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (!Convert.ToBoolean(filterContext.HttpContext.Session["Message"]))
            {
                filterContext.Result = new ContentResult()
                {
                    Content = ""
                };
            }

        }
    }
}
