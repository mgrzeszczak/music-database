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

        public AlbumController(IAlbumService albumService)
        {
            this.albumService = albumService;
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

        [Route("get/{id}")]
        [HttpGet]
        public Album Get(long id)
        {
            return albumService.FindById(id);
        }

        [Route("add")]
        [HttpPost]
        public Album Add([FromBody] Album album)
        {
            return albumService.Save(album);
        }

        [Route("update")]
        [HttpPut]
        public Album Update([FromBody] Album album)
        {
            return albumService.Update(album);
        }
    }
}
