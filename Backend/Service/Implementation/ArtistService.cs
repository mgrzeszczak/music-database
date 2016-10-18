using Backend.Repository;
using Common.Model;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Backend.Service
{
    public class ArtistService : BaseService<Artist,long,IArtistRepository>, IArtistService
    {

        public ArtistService(IArtistRepository repository) : base(repository)
        {
                    
        }

        public override IList<Artist> SearchBy(string searchText, int pageNr, int amountPerPage)
        {
            return repository.SearchBy(searchText, a => a.Name, pageNr, amountPerPage);
        }
    }
}
