using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class Transactional : Attribute
    {
           
    }
}