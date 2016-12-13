using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Backend.Scope;
using Backend.Service;
using Backend.Service.Interface;
using Common.Authorization;
using Common.Domain;
using Common.Message;
using Common.Model;
using Common.Model.Enum;
using Ninject;
using WebApi.Attributes;

namespace WebApi.Controllers
{
    [RoutePrefix("artist")]
    [@Authorize(Role.USER)]
    public class ArtistController : ApiController
    {
        private IArtistService artistService;
        private IAlbumService albumService;

        public ArtistController(IArtistService artistService, IAlbumService albumService)
        {
            this.artistService = artistService;
            this.albumService = albumService;
        }

        [HttpDelete]
        [Route("delete/{artistId}")]
        public void Delete(long artistId)
        {
            artistService.Delete(artistId);
        }

        [Route("search")]
        [HttpGet]
        public Page<Artist> Search(string searchText, int pageNr = 1, int amountPerPage = 10)
        {
            return artistService.SearchBy(searchText ?? "", pageNr, amountPerPage);
        }

        [Route("getWithAlbums/{id}")]
        [HttpGet]
        public ArtistWithAlbums GetWithAlbums(long id)
        {
            var artist = artistService.FindById(id);
            var albums = albumService.FindByArtistId(id);
            return new ArtistWithAlbums(artist,albums);
        }

        [Route("get/{id}")]
        [HttpGet]
        public Artist Get(long id)
        {
            var artist = artistService.FindById(id);
            return artist;
        }

        [Route("add")]
        [HttpPost]
        public Artist Add([FromBody, Valid] Artist artist)
        {
            return artistService.Save(artist);
        }

        [Route("update")]
        [HttpPut]
        public Artist Update([FromBody, Valid] Artist artist)
        {
            return artistService.Update(artist);
        }

        [Route("genres")]
        [HttpGet]
        public Genre[] GetAllGenres()
        {
            return Enum.GetValues(typeof(Genre)) as Genre[];
        }
    }
}
