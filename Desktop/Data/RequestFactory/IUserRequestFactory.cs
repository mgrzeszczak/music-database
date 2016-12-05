using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using RestSharp;

namespace Desktop.Data
{
    interface IUserRequestFactory
    {
        IRestRequest LoginRequest(string login, string password);
        IRestRequest LogoutRequest();
        IRestRequest UserDetailsRequest();
        IRestRequest RegisterRequest(User user);
    }
}
