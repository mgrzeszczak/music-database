using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Repository.Interface;
using Common.Model;

namespace Backend.Service.Interface
{
    public interface IUserService : IBaseService<User, long, IUserRepository>
    {
        User FindByLogin(string login);
    }
}
