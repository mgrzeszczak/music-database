using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Repository.Interface;
using Common.Model;

namespace Backend.Repository.Implementation
{
    public class UserRepository : BaseRepository<User, long>, IUserRepository
    {
        public User FindByLogin(string login)
        {
            return Queryable().FirstOrDefault(u => u.Login == login);
        }
    }
}
