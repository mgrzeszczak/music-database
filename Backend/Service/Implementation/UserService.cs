using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Repository.Interface;
using Backend.Service.Interface;
using Common.Domain;
using Common.Model;

namespace Backend.Service.Implementation
{
    public class UserService : BaseService<User, long, IUserRepository>, IUserService
    {
        public UserService(IUserRepository repository) : base(repository)
        {
            
        }

        public override Page<User> SearchBy(string searchText, int pageNr, int amountPerPage)
        {
            return repository.SearchBy(searchText, u => u.Login, pageNr, amountPerPage);
        }

        public override Page<User> SearchBy<TKey>(string searchText, Func<User, TKey> keyMapper, IComparer<TKey> comparator, int pageNr, int amountPerPage)
        {
            return repository.SearchBy(searchText, u => u.Login,keyMapper,comparator, pageNr, amountPerPage);
        }

        public User FindByLogin(string login)
        {
            return repository.FindByLogin(login);
        }
    }
}
