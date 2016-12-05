using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using RestSharp;

namespace Desktop.Data
{
    class UserRequestFactory : IUserRequestFactory
    {
        public IRestRequest LoginRequest(string login, string password)
        {
            throw new NotImplementedException();
        }

        public IRestRequest LogoutRequest()
        {
            throw new NotImplementedException();
        }

        public IRestRequest UserDetailsRequest()
        {
            throw new NotImplementedException();
        }

        public IRestRequest RegisterRequest(User user)
        {
            throw new NotImplementedException();
        }
    }
}
