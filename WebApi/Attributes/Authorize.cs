using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Authorization;
using WebApi.Authorization;

namespace WebApi.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false)]
    public class Authorize : Attribute
    {
        public Role Role { get; set; }

        public Authorize(Role role)
        {
            Role = role;
        }
    }
}