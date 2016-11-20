using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Backend.Service.Interface;
using Common.Authorization;
using Common.Domain;
using Common.Model;
using Common.Model.Enum;
using WebApi.Attributes;
using WebApi.Utils;

namespace WebApi.Controllers
{
    [RoutePrefix("comment")]
    [@Authorize(Role.USER)]
    public class CommentController : ApiController
    {
        private ICommentService commentService;

        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        [HttpDelete]
        [Route("delete/{commentId}")]
        [@Authorize(Role.ADMIN)]
        public void Delete(long commentId)
        {
            commentService.Delete(commentId);
        }

        [Route("find")]
        [HttpGet]
        public Page<Comment> Search(EntityType entityType, long entityId, int pageNr = 1, int amountPerPage = 10)
        {
            return commentService.PageByEntityIdAndType(entityId,entityType,pageNr,amountPerPage);
        }

        [Route("search")]
        [HttpGet]
        public Page<Comment> Search(string searchText, int pageNr = 1, int amountPerPage = 10)
        {
            return commentService.SearchBy(searchText, pageNr, amountPerPage);
        }

        [Route("get/{id}")]
        [HttpGet]
        public Comment Get(long id)
        {
            return commentService.FindById(id);
        }

        [Route("add")]
        [HttpPost]
        public Comment Add([FromBody, Valid] Comment comment)
        {
            comment.User = AuthenticationUtils.CurrentAuthentication.GetUser();
            return commentService.Save(comment);
        }

        
    }
}
