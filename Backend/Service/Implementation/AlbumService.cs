using Backend.Repository;
using Common.Model;
using System.Collections.Generic;
using Common.Domain;
using System;
using Ninject;

namespace Backend.Service
{
    public class AlbumService : BaseService<Album,long,IAlbumRepository>, IAlbumService
    {
        private IArtistRepository artistRepository;
        private ISongService songService;

        public AlbumService(IAlbumRepository repository, IArtistRepository artistRepository, ISongService songService) : base(repository)
        {
            this.artistRepository = artistRepository;
            this.songService = songService;
        }

        public Album Add(Album album)
        {
            Artist artist = this.artistRepository.FindOne(album.Artist.Id);
            album.Artist = artist;
            album =  this.Save(album);
            return album;
        }

        public override Page<Album> SearchBy(string searchText, int pageNr, int amountPerPage)
        {
            return repository.SearchBy(searchText, a => a.Name+a.Artist.Name, pageNr, amountPerPage);
        }

        public IList<Album> FindByArtistId(long artistId)
        {
            IList<Album> albums = repository.FindByArtistId(artistId);
            return albums;
        }

        public override Album Update(Album album)
        {
            Artist artist = artistRepository.FindOne(album.Artist.Id);
            album.Artist = artist;
            repository.Update(album);
            return album;
        }

        public override void Delete(long id)
        {
            IList<Song> songs = songService.FindByAlbumId(id);
            foreach (var song in songs) songService.Delete(song.Id);
            base.Delete(id);
        }

        public override Page<Album> SearchBy<TKey>(string searchText, Func<Album, TKey> keyMapper, IComparer<TKey> comparator, int pageNr, int amountPerPage)
        {
            return repository.SearchBy(searchText, a => a.Name + a.Artist.Name,keyMapper,comparator, pageNr, amountPerPage);
        }
    }
}
