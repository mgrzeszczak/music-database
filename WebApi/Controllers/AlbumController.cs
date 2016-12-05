using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Backend.Service;
using Backend.Service.Interface;
using Common.Authorization;
using Common.Domain;
using Common.Message;
using Common.Model;
using Ninject;
using WebApi.Attributes;

namespace WebApi.Controllers
{
    [RoutePrefix("album")]
    [@Authorize(Role.USER)]
    public class AlbumController : ApiController
    {
        private IAlbumService albumService;
        private ISongService songService;

        public AlbumController(IAlbumService albumService, ISongService songService)
        {
            this.albumService = albumService;
            this.songService = songService;
        }
        
        [HttpDelete]
        [Route("delete/{albumId}")]
        public void Delete(long albumId)
        {
            albumService.Delete(albumId);
        }

        [HttpGet]
        [Route("byArtist/{artistId}")]
        public IList<Album> FindByArtistId(long artistId)
        {
            return albumService.FindByArtistId(artistId);
        }

        [Route("search")]
        [HttpGet]
        public Page<Album> Search(string searchText, int pageNr = 1, int amountPerPage = 10)
        {
            return albumService.SearchBy(searchText, pageNr, amountPerPage);
        }

        [Route("getWithSongs/{id}")]
        [HttpGet]
        public AlbumWithSongs GetWithAlbums(long id)
        {
            var album = albumService.FindById(id);
            var songs = songService.FindByAlbumId(id);
            return new AlbumWithSongs(album,songs);
        }

        [Route("get/{id}")]
        [HttpGet]
        public Album Get(long id)
        {
            return albumService.FindById(id);
        }

        [Route("add")]
        [HttpPost]
        public Album Add([FromBody, Valid] Album album)
        {
            return albumService.Save(album);
        }

        [Route("update")]
        [HttpPut]
        public Album Update([FromBody, Valid] Album album)
        {
            return albumService.Update(album);
        }
    }
}
