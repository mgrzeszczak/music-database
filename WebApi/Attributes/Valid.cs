using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class Valid : Attribute
    {

    }
}