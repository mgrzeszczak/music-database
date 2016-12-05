using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using Common.Model.Enum;
using RestSharp;

namespace Desktop.Data
{
    class CommentRequestFactory : ICommentRequestFactory
    {
        public IRestRequest GetCommentRequest(long id)
        {
            throw new NotImplementedException();
        }

        public IRestRequest AddCommentRequest(Comment comment)
        {
            throw new NotImplementedException();
        }

        public IRestRequest FindCommentsRequest(EntityType entityType, long entityId, int pageNr = 1, int amountPerPage = 10)
        {
            throw new NotImplementedException();
        }

        public IRestRequest DeleteCommentRequest(long id)
        {
            throw new NotImplementedException();
        }

        public IRestRequest SearchCommentsRequest(string searchText, int pageNr = 1, int amountPerPage = 10)
        {
            throw new NotImplementedException();
        }
    }
}
