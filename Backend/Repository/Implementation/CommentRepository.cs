using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Repository.Interface;
using Common.Domain;
using Common.Model;
using Common.Model.Enum;

namespace Backend.Repository.Implementation
{
    public class CommentRepository : BaseRepository<Comment, long>, ICommentRepository
    {
        public Page<Comment> PageByEntityIdAndType(long id, EntityType type, int pageNr, int amountPerPage)
        {
            var result = Queryable().Where(r => r.EntityId == id && r.EntityType == type).OrderByDescending(c=>c.TimeStamp);
            int count = result.Count();
            int pageCount = count / amountPerPage;
            if (count % amountPerPage != 0) pageCount += 1;
            return Page(pageNr, amountPerPage, c => c.TimeStamp, Comparer<DateTime>.Create((a,b)=>a>b?-1:a==b?0:1), result, count, pageCount);
        }

        public Page<Comment> PageByEntityIdAndTypeAndUserId(long id, EntityType type, long userId, int pageNr, int amountPerPage)
        {
            var result = Queryable().Where(r => r.EntityId == id && r.EntityType == type && r.User.Id == userId).OrderByDescending(c => c.TimeStamp);
            int count = result.Count();
            int pageCount = count / amountPerPage;
            if (count % amountPerPage != 0) pageCount += 1;
            return Page(pageNr, amountPerPage, c => c.TimeStamp, Comparer<DateTime>.Default, result, count, pageCount);
        }
    }
}
