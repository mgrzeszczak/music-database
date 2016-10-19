using Backend.Repository;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service
{
    public interface ISongService : IBaseService<Song, long, ISongRepository>
    {
        Song Add(Song song, long albumId);
    }
}
