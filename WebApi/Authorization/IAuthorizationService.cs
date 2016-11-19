using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Authorization;
using Common.Model;

namespace WebApi.Authorization
{
    public interface IAuthorizationService
    {

        Authentication Login(string login, string password);
        User Register(User user);
        void Logout();

    }
}
