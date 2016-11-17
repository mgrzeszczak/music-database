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
    public class RatingService : BaseService<Rating, long, IRatingRepository>, IRatingService
    {
        public RatingService(IRatingRepository repository) : base(repository)
        {
        }

        public override Page<Rating> SearchBy(string searchText, int pageNr, int amountPerPage)
        {
            return repository.SearchBy(searchText, r=>r.Value.ToString(), pageNr, amountPerPage);
        }

        public override Page<Rating> SearchBy<TKey>(string searchText, Func<Rating, TKey> keyMapper, IComparer<TKey> comparator, int pageNr, int amountPerPage)
        {
            return repository.SearchBy(searchText, r => r.Value.ToString(), keyMapper, comparator, pageNr, amountPerPage);
        }
    }
}
