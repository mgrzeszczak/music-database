using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exception
{
    public class NetworkException : System.Exception
    {
        public NetworkException(String message) : base(message)
        {

        }

        public NetworkException(String message, System.Exception innerException) : base(message,innerException)
        {

        }
    }
}
