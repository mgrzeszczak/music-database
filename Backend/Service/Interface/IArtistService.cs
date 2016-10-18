using Backend.Repository;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service
{
    public interface IArtistService : IBaseService<Artist, long,IArtistRepository>
    {

    }
}
