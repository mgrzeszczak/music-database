using Common.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Service
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
