using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace WebApi.Authorization
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