using Common.Model;
using System.Collections.Generic;

namespace Backend.Repository
{
    public interface ISongRepository : IBaseRepository<Song,long>
    {
        IList<Song> FindAllByAlbumId(long albumId);
    }
}
