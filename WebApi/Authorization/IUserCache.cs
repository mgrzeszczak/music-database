using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace WebApi.Authorization
{
    public interface IUserCache
    {
        void PutLoginWithToken(string login, string token);
        void PutUserWithToken(string token, IAuthentication authentication);
        string GetTokenByLogin(string login);
        IAuthentication GetUserByToken(string token);
        void RemoveUser(IAuthentication authentication);
    }
}