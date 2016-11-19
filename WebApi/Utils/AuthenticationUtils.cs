using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using Common.Authorization;
using WebApi.Authorization;

namespace WebApi.Utils
{
    public class AuthenticationUtils
    {
        private static ThreadLocal<IAuthentication> currentAuthentication = new ThreadLocal<IAuthentication>();

        public static IAuthentication CurrentAuthentication
        {
            get { return currentAuthentication.Value; }
            set { currentAuthentication.Value = value; }
        }

    }
}