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
using Common.Model;
using Ninject;
using WebApi.Attributes;

namespace WebApi.Controllers
{
    [RoutePrefix("artist")]
    [@Authorize(Role.USER)]
    public class ArtistController : ApiController
    {
        private IArtistService artistService;

        public ArtistController(IArtistService artistService)
        {
            this.artistService = artistService;
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
            return artistService.SearchBy(searchText, pageNr, amountPerPage);
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
        public Artist Add([FromBody] Artist album)
        {
            return artistService.Save(album);
        }

        [Route("update")]
        [HttpPut]
        public Artist Update([FromBody] Artist album)
        {
            return artistService.Update(album);
        }
    }
}
