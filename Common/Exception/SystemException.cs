using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exception
{
    public class SystemException : System.Exception
    {

        public SystemException(String message) : base(message)
        {

        }
        public SystemException(String message, System.Exception innerException) : base(message,innerException)
        {
            
        }
    }
}
