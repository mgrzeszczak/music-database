using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain
{
    public class Response<T>
    {
        public bool Status { get; }
        public T Content { get; }
        public Error Error { get; }

        public Response(Error error)
        {
            this.Status = false;
            this.Error = error;
        }

        public Response(T content)
        {
            this.Content = content;
            this.Status = true;
        }
    }
}
