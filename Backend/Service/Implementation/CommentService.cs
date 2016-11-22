using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Repository.Interface;
using Backend.Service.Interface;
using Common.Domain;
using Common.Model;
using Common.Model.Enum;

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

        public Page<Comment> PageByEntityIdAndType(long id, EntityType type, int pageNr, int amountPerPage)
        {
            return repository.PageByEntityIdAndType(id, type, pageNr, amountPerPage);
        }

        public Page<Comment> PageByEntityIdAndTypeAndUserId(long id, EntityType type, long userId, int pageNr, int amountPerPage)
        {
            return repository.PageByEntityIdAndTypeAndUserId(id, type, userId, pageNr, amountPerPage);
        }

        public override Comment Save(Comment t)
        {
            t.TimeStamp = DateTime.Now;
            return base.Save(t);
        }
    }
}
