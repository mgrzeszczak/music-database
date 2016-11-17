using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Backend.Service.Interface;
using Common.Authorization;
using Common.Domain;
using Common.Exception;
using Common.Model;
using Ninject;

namespace WebApi.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        private IUserCache userCache;
        private IUserService userService;

        public AuthorizationService(IUserCache userCache, IUserService userService)
        {
            this.userCache = userCache;
            this.userService = userService;
        }

        public Authentication Login(string login, string password)
        {
            var user = userService.FindByLogin(login);
            if (user==null || StringUtils.CalculateHashedPassword(password,user.PasswordSalt)!=user.Password) throw new BusinessException(Error.INVALID_CREDENTIALS);
            var token = StringUtils.GenerateToken();
            var auth =  new Authentication(user, token);
            userCache.PutLoginWithToken(login, token);
            userCache.PutUserWithToken(token,auth);
            return auth;
        }

        public User Register(User user)
        {
            user.PasswordSalt = StringUtils.GenerateSalt();
            user.Password = StringUtils.CalculateHashedPassword(user.Password, user.PasswordSalt);
            user.Role = Role.USER;
            return userService.Save(user);
        }

        public void Logout()
        {
            var auth = AuthenticationUtils.CurrentAuthentication;
            userCache.RemoveUser(auth);
            AuthenticationUtils.CurrentAuthentication = null;
        }
    }
}