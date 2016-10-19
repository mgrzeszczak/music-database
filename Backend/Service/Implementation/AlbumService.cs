using Backend.Repository;
using Common.Model;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Backend.Service
{
    public class AlbumService : BaseService<Album,long,IAlbumRepository>, IAlbumService
    {
        private IArtistService artistService;

        public AlbumService(IAlbumRepository repository, IArtistService artistService) : base(repository)
        {
            this.artistService = artistService;
        }

        public Album Add(Album album, long artistId)
        {
            Artist artist = this.artistService.FindById(artistId);
            album.Artist = artist;
            return this.Save(album);
        }

        public override IList<Album> SearchBy(string searchText, int pageNr, int amountPerPage)
        {
            return repository.SearchBy(searchText, a => a.Name, pageNr, amountPerPage);
        }
    }
}
