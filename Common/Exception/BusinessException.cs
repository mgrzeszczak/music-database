using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exception
{
    public class BusinessException : System.Exception
    {

        public BusinessException(String message) : base(message)
        {

        }
        public BusinessException(String message, System.Exception innerException) : base(message,innerException)
        {

        }
    }
}
