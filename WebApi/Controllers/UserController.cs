using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Backend.Service.Interface;
using Common.Authorization;
using Common.Model;
using Ninject;
using WebApi.Attributes;
using WebApi.Authorization;
using WebApi.Utils;

namespace WebApi.Controllers
{
    [RoutePrefix("user")]
    public class UserController : ApiController
    {
        private IAuthorizationService authorizationService;

        public UserController(IAuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService;
        }

        [Route("login")]
        [HttpPost]
        public Authentication Login([FromBody] User user)
        {
            var auth = authorizationService.Login(user.Login,user.Password);
            return auth;
        }

        [Route("register")]
        [HttpPost]
        public User Register([FromBody] User user)
        {
            return authorizationService.Register(user);
        }

        [@Authorize(Role.USER)]
        [Route("logout")]
        [HttpPost]
        public void Logout()
        {
            authorizationService.Logout();
        }

        [@Authorize(Role.USER)]
        [Route("details")]
        [HttpGet]
        public User UserDetails()
        {
            return AuthenticationUtils.CurrentAuthentication.GetUser();
        }

    }
}
