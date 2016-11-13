using System;
using System.Collections.Generic;
using Common.Model;
using System.Linq;
namespace Backend.Repository
{
    public class SongRepository : BaseRepository<Song, long>, ISongRepository
    {
        public IList<Song> FindAllByAlbumId(long albumId)
        {
            return Queryable().Where((s) => s.Album.Id == albumId).ToList();
        }

        
    }
}
