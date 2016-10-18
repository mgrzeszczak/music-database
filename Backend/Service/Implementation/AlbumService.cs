using Backend.Repository;
using Common.Model;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Backend.Service
{
    public class AlbumService : BaseService<Album,long,IAlbumRepository>, IAlbumService
    {
        public AlbumService(IAlbumRepository repository) : base(repository)
        {

        }

        public override IList<Album> SearchBy(string searchText, int pageNr, int amountPerPage)
        {
            return repository.SearchBy(searchText, a => a.Name, pageNr, amountPerPage);
        }
    }
}
