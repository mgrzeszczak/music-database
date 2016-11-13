using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exception
{
    public class BusinessException : System.Exception
    {
        public Error Error { get; }

        public BusinessException(String message) : base(message)
        {
            Error = Error.UNKNOWN_ERROR;
        }
        public BusinessException(String message, System.Exception innerException) : base(message,innerException)
        {
            Error = Error.UNKNOWN_ERROR;
        }
        public BusinessException(Error error)
        {
            Error = error;
        }
    }
}
