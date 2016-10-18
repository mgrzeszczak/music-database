using Backend.Repository;
using Common.Model;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Backend.Service
{
    public class SongService : BaseService<Song,long,ISongRepository>, ISongService
    {
        public SongService(ISongRepository repository) : base(repository)
        {
            
        }

        public override IList<Song> SearchBy(string searchText, int pageNr, int amountPerPage)
        {
            return repository.SearchBy(searchText, s => s.Title, pageNr, amountPerPage);
        }
    }
}
