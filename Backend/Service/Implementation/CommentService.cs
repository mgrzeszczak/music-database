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
    public class CommentService : BaseService<Comment, long, ICommentRepository>, ICommentService
    {
        public CommentService(ICommentRepository repository) : base(repository)
        {

        }

        public override Page<Comment> SearchBy(string searchText, int pageNr, int amountPerPage)
        {
            return repository.SearchBy(searchText, c=>c.Content+c.User.Login, pageNr, amountPerPage);
        }

        public override Page<Comment> SearchBy<TKey>(string searchText, Func<Comment, TKey> keyMapper, IComparer<TKey> comparator, int pageNr, int amountPerPage)
        {
            return repository.SearchBy(searchText, c=>c.Content+c.User.Login, keyMapper, comparator, pageNr, amountPerPage);
        }
    }
}
