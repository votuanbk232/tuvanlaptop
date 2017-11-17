using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuVanLaptoop.Models;

namespace TuVanLaptoop.Filter
{
    public class AdminFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.HttpContext.Session["IsAdmin"] ==null)
            //if (!Convert.ToBoolean(filterContext.HttpContext.Session["IsAdmin"] as Admin))
            {
                filterContext.Result = new ContentResult()
                {
                    Content = "Chỉ admin được quyền truy cập!"
                };
            }

        }

    }

}