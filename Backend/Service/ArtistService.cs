using Backend.Repository;
using Common.Model;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Service
{
    public class ArtistService
    {
        private ArtistRepository artistRepository;

        public ArtistService(ArtistRepository artistRepository)
        {
            this.artistRepository = artistRepository;
        }

        public IList<Artist> searchArtists(string text)
        {
            return artistRepository.query().Where(s => s.Name.Contains(text)).ToList();
        }

        public void update(Artist artist)
        {
            this.artistRepository.update(artist);
        }

        public Artist save(Artist artist)
        {
            return this.artistRepository.save(artist);
        }
    }
}
