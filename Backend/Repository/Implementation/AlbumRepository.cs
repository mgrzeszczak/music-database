using Common.Model;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Backend.Repository
{
    public class AlbumRepository : BaseRepository<Album,long>, IAlbumRepository
    {
        public IList<Album> FindByArtistId(long artistId)
        {
            return Queryable().Where((s) => s.Artist.Id == artistId).ToList();
        }
        
    }
}
