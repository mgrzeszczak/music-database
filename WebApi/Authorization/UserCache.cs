using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using Common.Authorization;
using Common.Domain;
using Common.Exception;
using Common.Model;

namespace WebApi.Authorization
{
    public class UserCache : IUserCache
    {
        private readonly Dictionary<string, IAuthentication> loginAuthDict = new Dictionary<string, IAuthentication>();
        private readonly Dictionary<string, DateTime> loginLastTimeDict = new Dictionary<string, DateTime>();

        private TimeSpan TOKEN_EXPIRATION_TIME = TimeSpan.FromMinutes(5);

        public UserCache()
        {
            User testUser = new User();
            testUser.Role= Role.ADMIN;
            testUser.Login = "login";
            Authentication authentication = new Authentication(testUser,"token");
            loginAuthDict.Add(testUser.Login,authentication);
            loginLastTimeDict.Add(testUser.Login,new DateTime(2017,1,1,1,1,1));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void PutAuthWithLogin(string login, IAuthentication auth)
        {
            if (loginAuthDict.ContainsKey(login)) loginAuthDict.Remove(login);
            if (loginLastTimeDict.ContainsKey(login)) loginLastTimeDict.Remove(login);
            loginAuthDict.Add(login,auth);
            loginLastTimeDict.Add(login,DateTime.Now);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IAuthentication GetAuthByLogin(string login)
        {
            /*
            if (loginLastTimeDict.ContainsKey(login))
            {
                var lastTime = loginLastTimeDict[login];
                if (DateTime.Now - lastTime > TOKEN_EXPIRATION_TIME)
                {
                    loginAuthDict.Remove(login);
                    loginLastTimeDict.Remove(login);
                    return null;
                }
                loginLastTimeDict.Remove(login);
                loginLastTimeDict.Add(login, new DateTime());
            }*/
            return loginAuthDict.ContainsKey(login) ? loginAuthDict[login] : null;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void RemoveAuth(string login)
        {
            loginAuthDict.Remove(login);
            loginLastTimeDict.Remove(login);
        }

    }
}