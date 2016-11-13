using Backend.Repository;
using Common.Model;
using System.Collections.Generic;
using System.Linq;
using System;
using Common.Domain;
using Ninject;

namespace Backend.Service
{
    public class ArtistService : BaseService<Artist,long,IArtistRepository>, IArtistService
    {
        private IAlbumService albumService;

        public ArtistService(IArtistRepository repository, IAlbumService albumService) : base(repository)
        {
            this.albumService = albumService;
        }

        public override Page<Artist> SearchBy(string searchText, int pageNr, int amountPerPage)
        {
            return repository.SearchBy(searchText, a => a.Name, pageNr, amountPerPage);
        }

        public override void Delete(long id)
        {
            IList<Album> albums = albumService.FindByArtistId(id);
            foreach (var album in albums)
            {
                albumService.Delete(album.Id);
            }
            base.Delete(id);
        }

        public override Page<Artist> SearchBy<TKey>(string searchText, Func<Artist, TKey> keyMapper, IComparer<TKey> comparator, int pageNr, int amountPerPage)
        {
            return repository.SearchBy(searchText, a => a.Name,keyMapper,comparator, pageNr, amountPerPage);
        }
    }
}
