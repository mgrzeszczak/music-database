using Backend.Repository;
using Common.Model;
using System.Collections.Generic;
using System.Linq;
using System;
using Common.Domain;
using Ninject;

namespace Backend.Service
{
    public class SongService : BaseService<Song,long,ISongRepository>, ISongService
    {
        private IAlbumRepository albumRepository;

        public SongService(ISongRepository repository, IAlbumRepository albumRepository) : base(repository)
        {
            this.albumRepository = albumRepository;
        }

        public Song Add(Song song)
        {
            Album album = this.albumRepository.FindOne(song.Album.Id);
            song.Album = album;
            song = this.Save(song);
            return song;
        }

        public IList<Song> FindByAlbumId(long albumId)
        {
            IList<Song> songs = repository.FindAllByAlbumId(albumId);
            return songs;
        }

        public override Page<Song> SearchBy(string searchText, int pageNr, int amountPerPage)
        {
            return repository.SearchBy(searchText, s => s.Title+s.Album.Name+s.Album.Artist.Name, pageNr, amountPerPage);
        }

        public override Page<Song> SearchBy<TKey>(string searchText, Func<Song, TKey> keyMapper, IComparer<TKey> comparator, int pageNr, int amountPerPage)
        {
            return repository.SearchBy(searchText, s => s.Title + s.Album.Name + s.Album.Artist.Name,keyMapper,comparator, pageNr, amountPerPage);
        }

        public override Song Update(Song song)
        {
            Album album = albumRepository.FindOne(song.Album.Id);
            song.Album = album;
            repository.Update(song);
            return song;
        }

    }
}
