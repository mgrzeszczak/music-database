﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Authorization;
using Common.Model;

namespace WebApi.Authorization
{
    public interface IAuthentication
    {
        User GetUser();
        Role GetRole();
        string GetLogin();
        string GetToken();
    }
}