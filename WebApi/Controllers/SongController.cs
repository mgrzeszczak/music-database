using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Backend.Service;
using Backend.Service.Interface;
using Common.Authorization;
using Common.Domain;
using Common.Exception;
using Common.Model;
using Ninject;
using WebApi.App_Start;
using WebApi.Attributes;
using WebApi.Authorization;

namespace WebApi.Controllers
{
    [RoutePrefix("song")]
    [@Authorize(Role.USER)]
    public class SongController : ApiController
    {
        private ISongService songService;
        private IRatingService ratingService;
        private ICommentService commentService;

        public SongController(ISongService songService, IRatingService ratingService, ICommentService commentService)
        {
            this.songService = songService;
            this.ratingService = ratingService;
            this.commentService = commentService;
        }

        [HttpDelete]
        [Route("delete/{songId}")]
        public void Delete(long songId)
        {
            songService.Delete(songId);
        }
           
        [HttpGet]
        [Route("find/{albumId}")]
        public IList<Song> FindByAlbumId(long albumId)
        {
            return songService.FindByAlbumId(albumId);
        }

        [Route("search")]
        [HttpGet]
        public Page<Song> Search(string searchText, int pageNr = 1, int amountPerPage= 10)
        {
            return songService.SearchBy(searchText, pageNr, amountPerPage);
        }

        [Route("get/{id}")]
        [HttpGet]
        public Song Get(long id)
        {
            return songService.FindById(id);
        }
        
        [Route("add")]
        [HttpPost]
        public Song Add([FromBody] Song song)
        {
            return songService.Save(song);
        }

        [Route("update")]
        [HttpPut]
        public Song Update([FromBody] Song song)
        {
            return songService.Update(song);
        }
    }
}
