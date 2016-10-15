using Common.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Service
{
    public class ArtistService
    {
        private ArtistRepository artistRepository;

        public ArtistService(ArtistRepository artistRepository)
        {
            this.artistRepository = artistRepository;
        }
    }
}
