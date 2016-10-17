using Backend.Repository;
using Common.Model;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Service
{
    public class AlbumService
    {
        private AlbumRepository albumRepository;

        public AlbumService(AlbumRepository albumRepository)
        {
            this.albumRepository = albumRepository;
        }

        public IList<Album> searchAlbums(string text)
        {
            return this.albumRepository.query().Where(s => s.Name.Contains(text)).ToList();
        }

        public void update(Album album)
        {
            this.albumRepository.update(album);
        }

        public Album save(Album album)
        {
            return this.albumRepository.save(album);
        }

    }
}
