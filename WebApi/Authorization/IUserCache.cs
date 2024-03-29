﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Authorization;


namespace WebApi.Authorization
{
    public interface IUserCache
    {
        void PutAuthWithLogin(string login, IAuthentication auth);
        IAuthentication GetAuthByLogin(string login);
        void RemoveAuth(string login);
    }
}