using Backend.Repository;
using Common.Model;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Backend.Service
{
    public class SongService : BaseService<Song,long,ISongRepository>, ISongService
    {
        private IAlbumService albumService;

        public SongService(ISongRepository repository, IAlbumService albumService) : base(repository)
        {
            this.albumService = albumService;
        }

        public Song Add(Song song, long albumId)
        {
            Album album = this.albumService.FindById(albumId);
            song.Album = album;
            return this.Save(song);
        }

        public override IList<Song> SearchBy(string searchText, int pageNr, int amountPerPage)
        {
            return repository.SearchBy(searchText, s => s.Title, pageNr, amountPerPage);
        }
    }
}
