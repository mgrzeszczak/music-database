using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Common.Authorization;
using WebApi.App_Start;
using WebApi.Attributes;
using WebApi.Authorization;
using WebApi.Utils;

namespace WebApi.Filters
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

            IEnumerable<string> authTokenHeaders;
            IEnumerable<string> authLoginHeaders;

            var foundTokenHeaders = headers.TryGetValues("AUTH-TOKEN", out authTokenHeaders);
            var foundLoginHeaders = headers.TryGetValues("AUTH-LOGIN", out authLoginHeaders);

            if (!foundLoginHeaders || !foundTokenHeaders)
            {
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                actionContext.Response = response;
                return;
            }

            var token = authTokenHeaders.FirstOrDefault();
            var login = authLoginHeaders.FirstOrDefault();

            if (token == null || login == null || !Authorize(requiredRole, login, token))
            {
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                actionContext.Response = response;
            }
        }

        private bool Authorize(Role role, string login, string token)
        {
            IUserCache userCache = NinjectHttpContainer.Resolve<IUserCache>();
            var auth = userCache.GetAuthByLogin(login);
            //var cachedToken = userCache.GetTokenByLogin(login);
            //var user = userCache.GetUserByToken(token);
            if (auth == null || auth.GetUser() == null || auth.GetToken() == null || auth.GetToken()!=token || auth.GetLogin()!=login) return false;
            var ret = auth.GetRole() >= role;
            if (ret) AuthenticationUtils.CurrentAuthentication = auth;
            return ret;
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            
        }

    }
}