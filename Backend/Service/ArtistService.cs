using Backend.Repository;

namespace Backend.Service
{
    public class ArtistService
    {
        private ArtistRepository artistRepository;

        public ArtistService(ArtistRepository artistRepository)
        {
            this.artistRepository = artistRepository;
        }
    }
}
