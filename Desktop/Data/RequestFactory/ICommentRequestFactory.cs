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
    interface ICommentRequestFactory
    {
        IRestRequest GetCommentRequest(long id);
        IRestRequest AddCommentRequest(Comment comment);
        IRestRequest FindCommentsRequest(EntityType entityType, long entityId, int pageNr = 1, int amountPerPage = 10);
        IRestRequest DeleteCommentRequest(long id);
        IRestRequest SearchCommentsRequest(string searchText, int pageNr = 1, int amountPerPage = 10);
    }
}
