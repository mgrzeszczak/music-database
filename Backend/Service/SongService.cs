using Backend.Repository;
using Common.Model;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Service
{
    public class SongService
    {
        private SongRepository songRepository;

        public SongService(SongRepository songRepository)
        {
            this.songRepository = songRepository;
        }

        public IList<Song> searchSongs(string text)
        {
            return songRepository.query().Where(s => s.Title.Contains(text)).ToList();
        }

        public void update(Song song)
        {
            this.songRepository.update(song);
        }
        

    }
}
