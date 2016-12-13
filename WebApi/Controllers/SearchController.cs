using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Backend.Service;
using Common.Authorization;
using Common.Domain;
using Common.Model;
using WebApi.Attributes;

namespace WebApi.Controllers
{
    [RoutePrefix("search")]
    [@Authorize(Role.USER)]
    public class SearchController : ApiController
    {
        private IArtistService artistService;
        private IAlbumService albumService;
        private ISongService songService;

        public SearchController(IArtistService artistService, IAlbumService albumService, ISongService songService)
        {
            this.artistService = artistService;
            this.albumService = albumService;
            this.songService = songService;
        }

        [HttpGet]
        [Route("all")]
        public CommonSearchResult Search(string searchText, int pageNr = 1, int amountPerPage = 10)
        {
            return new CommonSearchResult(songService.SearchBy(searchText??"",pageNr,amountPerPage),
                                          albumService.SearchBy(searchText??"", pageNr, amountPerPage),
                                          artistService.SearchBy(searchText??"", pageNr, amountPerPage));
        }

    }
}
