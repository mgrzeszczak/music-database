using Backend.Repository;
using Common.Model;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Service
{
    public class SongService : BaseService<Song,long,ISongRepository>, ISongService
    {
        public SongService(ISongRepository repository) : base(repository)
        {
            
        }
        /*
        public IList<Song> searchSongs(string text)
        {
            return repository.Queryable().Where(s => s.Title.Contains(text)).ToList();
        }

        public void update(Song song)
        {
            this.songRepository.update(song);
        }
        */
        

    }
}
