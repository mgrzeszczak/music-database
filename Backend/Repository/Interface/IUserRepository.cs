using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;

namespace Backend.Repository.Interface
{
    public interface IUserRepository : IBaseRepository<User,long>
    {
        User FindByLogin(string login);
    }
}
