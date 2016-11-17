using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Backend.Service;
using Backend.Service.Interface;
using Common.Authorization;
using Ninject;
using WebApi.Attributes;

namespace WebApi.Controllers
{
    [RoutePrefix("album")]
    [@Authorize(Role.USER)]
    public class AlbumController : ApiController
    {
        private IAlbumService albumService;
        private IRatingService ratingService;
        private ICommentService commentService;

        public AlbumController(IAlbumService albumService, IRatingService ratingService, ICommentService commentService)
        {
            this.albumService = albumService;
            this.ratingService = ratingService;
            this.commentService = commentService;
        }
    }
}
