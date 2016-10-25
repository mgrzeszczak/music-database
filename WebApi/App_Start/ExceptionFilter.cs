using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace WebApi.App_Start
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {

        public override void OnException(HttpActionExecutedContext context)
        {
           
        }
    }
}