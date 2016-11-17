using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Authorization;
using Common.Model;

namespace WebApi.Authorization
{
    public class Authentication : IAuthentication
    {
        public User User { get; }
        public string Token { get; }

        public Authentication(User user, string token)
        {
            User = user;
            Token = token;
        }

        public User GetUser()
        {
            return User;
        }

        public Role GetRole()
        {
            return User.Role;
        }

        public string GetLogin()
        {
            return User.Login;
        }

        public string GetToken()
        {
            return Token;
        }
    }
}