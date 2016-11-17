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
    [RoutePrefix("artist")]
    [@Authorize(Role.USER)]
    public class ArtistController : ApiController
    {
        private IArtistService artistService;
        private IRatingService ratingService;
        private ICommentService commentService;

        public ArtistController(IArtistService artistService, IRatingService ratingService, ICommentService commentService)
        {
            this.artistService = artistService;
            this.ratingService = ratingService;
            this.commentService = commentService;
        }
    }
}
