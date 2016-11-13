using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository
{
    public interface IAlbumRepository : IBaseRepository<Album,long>
    {
        IList<Album> FindByArtistId(long id);
    }
}
