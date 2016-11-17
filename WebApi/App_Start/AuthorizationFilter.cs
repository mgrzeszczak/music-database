using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Common.Authorization;
using WebApi.Attributes;
using WebApi.Authorization;

namespace WebApi.App_Start
{
    public class AuthorizationFilter : ActionFilterAttribute
    {

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var headers = actionContext.Request.Headers;
            var controllerAttribute = actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<Authorize>(false).FirstOrDefault();
            var actionAttribute = actionContext.ActionDescriptor.GetCustomAttributes<Authorize>(false).FirstOrDefault();

            if (actionAttribute == null && controllerAttribute == null)
            {
                AuthenticationUtils.CurrentAuthentication = null;
                return;
            }

            var requiredRole = actionAttribute?.Role ?? controllerAttribute.Role;

            var token = headers.GetValues("AUTH-TOKEN").FirstOrDefault();
            var login = headers.GetValues("AUTH-LOGIN").FirstOrDefault();

            if (token == null || login == null || !Authorize(requiredRole, login, token))
            {
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                actionContext.Response = response;
            }
        }

        private bool Authorize(Role role, string login, string token)
        {
            IUserCache userCache = NinjectHttpContainer.Resolve<IUserCache>();
            var cachedToken = userCache.GetTokenByLogin(login);
            var user = userCache.GetUserByToken(token);
            if (user == null || cachedToken == null || token!=cachedToken || user.GetLogin()!=login) return false;
            var ret = user.GetRole() >= role;
            if (ret) AuthenticationUtils.CurrentAuthentication = user;
            return ret;
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            
        }

    }
}