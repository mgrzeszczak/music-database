using Backend.Repository;

namespace Backend.Service
{
    public class AlbumService
    {
        private AlbumRepository albumRepository;

        public AlbumService(AlbumRepository albumRepository)
        {
            this.albumRepository = albumRepository;
        }
    }
}
