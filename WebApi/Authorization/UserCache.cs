using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Authorization;
using Common.Model;

namespace WebApi.Authorization
{
    public class UserCache : IUserCache
    {
        private readonly Dictionary<string,string> loginTokenDictionary = new Dictionary<string, string>();
        private readonly Dictionary<string, IAuthentication> tokenUserDictionary = new Dictionary<string, IAuthentication>();
        private readonly Dictionary<IAuthentication, DateTime> userToLastActionDictionary = new Dictionary<IAuthentication, DateTime>();

        public UserCache()
        {
            /*User testUser = new User();
            testUser.Role= Role.ADMIN;
            testUser.Login = "login";
            Authentication authentication = new Authentication(testUser,"token");
            loginTokenDictionary.Add("login","token");
            tokenUserDictionary.Add("token",authentication);*/
        }

        public void PutLoginWithToken(string login, string token)
        {
            loginTokenDictionary.Add(login,token);
        }

        public void PutUserWithToken(string token, IAuthentication authentication)
        {
            tokenUserDictionary.Add(token,authentication);
        }

        public string GetTokenByLogin(string login)
        {
            return loginTokenDictionary.ContainsKey(login)? loginTokenDictionary[login] : null;
        }

        public IAuthentication GetUserByToken(string token)
        {
            return tokenUserDictionary.ContainsKey(token) ? tokenUserDictionary[token] : null;
        }

        public void RemoveUser(IAuthentication authentication)
        {
            tokenUserDictionary.Remove(authentication.GetToken());
            loginTokenDictionary.Remove(authentication.GetLogin());
        }
    }
}