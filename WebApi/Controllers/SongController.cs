using Backend.Service;
using Common.Model;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class SongController : ApiController
    {
        private ISongService songService;
        private IArtistService artistService;

        public SongController(ISongService songService, IArtistService artistService)
        {
            this.songService = songService;
            this.artistService = artistService;
        }

        // GET api/values
        public bool Get()
        {
            Artist artist = new Artist();
            artist.Name = "Patrick Swayze";
            artist.Genre = Common.Model.Enum.Genre.POP;
            artistService.Save(artist);
            return true;
        }

        

    }
}
