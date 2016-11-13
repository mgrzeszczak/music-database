using System.Collections.Generic;
using Common.Model;
using Common.Communication;
using Common.Domain;
using Common.Exception;
using System.Threading;

namespace Backend.Communication
{
    public class BackendResponseDataProvider : IResponseDataProvider
    {
        private IDataProvider dataProvider;

        public BackendResponseDataProvider()
        {
            dataProvider = new BackendDataProvider();
        }

        public Response<bool> DeleteAlbum(long id)
        {
            try {
                dataProvider.DeleteAlbum(id);
            } catch (BusinessException e)
            {
                return new Response<bool>(e.Error);
            }
            return new Response<bool>(true);
        }

        public Response<bool> DeleteArtist(long id)
        {
            try
            {
                dataProvider.DeleteArtist(id);
            }
            catch (BusinessException e)
            {
                return new Response<bool>(e.Error);
            }
            return new Response<bool>(true);
        }

        public Response<bool> DeleteSong(long id)
        {
            try
            {
                dataProvider.DeleteSong(id);
            }
            catch (BusinessException e)
            {
                return new Response<bool>(e.Error);
            }
            return new Response<bool>(true);
        }

        public Response<Album> GetAlbumById(long id)
        {
            try
            {
                Album album = dataProvider.GetAlbumById(id);
                return new Response<Album>(album);
            }
            catch (BusinessException e)
            {
                return new Response<Album>(e.Error);
            }
        }

        public Response<IList<Album>> GetAlbumsByArtist(long artistId)
        {
            try
            {
                return new Response<IList<Album>>(dataProvider.GetAlbumsByArtist(artistId));
            }
            catch (BusinessException e)
            {
                return new Response<IList<Album>>(e.Error);
            }
        }

        public Response<Artist> GetArtistById(long id)
        {
            try
            {
                return new Response<Artist>(dataProvider.GetArtistById(id));
            }
            catch (BusinessException e)
            {
                return new Response<Artist>(e.Error);
            }
        }

        public Response<Song> GetSongById(long id)
        {
            try
            {
                return new Response<Song>(dataProvider.GetSongById(id));
            }
            catch (BusinessException e)
            {
                return new Response<Song>(e.Error);
            }
        }

        public Response<IList<Song>> GetSongsFromAlbum(long albumId)
        {
            try
            {
                return new Response<IList<Song>>(dataProvider.GetSongsFromAlbum(albumId));
            }
            catch (BusinessException e)
            {
                return new Response<IList<Song>>(e.Error);
            }
        }

        public Response<Album> SaveAlbum(Album album)
        {
            try
            {
                return new Response<Album>(dataProvider.SaveAlbum(album));
            }
            catch (BusinessException e)
            {
                return new Response<Album>(e.Error);
            }
        }

        public Response<Artist> SaveArtist(Artist artist)
        {
            try
            {
                return new Response<Artist>(dataProvider.SaveArtist(artist));
            }
            catch (BusinessException e)
            {
                return new Response<Artist>(e.Error);
            }
        }

        public Response<Song> SaveSong(Song song)
        {
            try
            {
                return new Response<Song>(dataProvider.SaveSong(song));
            }
            catch (BusinessException e)
            {
                return new Response<Song>(e.Error);
            }
        }

        public Response<Page<Album>> SearchAlbumsByName(string searchText, int pageNr = 1, int amountPerPage = 50)
        {
            try
            {
                return new Response<Page<Album>>(dataProvider.SearchAlbumsByName(searchText,pageNr,amountPerPage));
            }
            catch (BusinessException e)
            {
                return new Response<Page<Album>>(e.Error);
            }
        }

        public Response<Page<Artist>> SearchArtistsByName(string searchText, int pageNr = 1, int amountPerPage = 50)
        {
            try
            {
                return new Response<Page<Artist>>(dataProvider.SearchArtistsByName(searchText,pageNr,amountPerPage));
            }
            catch (BusinessException e)
            {
                return new Response<Page<Artist>>(e.Error);
            }
        }

        public Response<Page<Song>> SearchSongsByTitle(string searchText, int pageNr = 1, int amountPerPage = 50)
        {
            try
            {
                return new Response<Page<Song>>(dataProvider.SearchSongsByTitle(searchText,pageNr,amountPerPage));
            }
            catch (BusinessException e)
            {
                return new Response<Page<Song>>(e.Error);
            }
        }

        public Response<Album> UpdateAlbum(Album album)
        {
            try
            {
                return new Response<Album>(dataProvider.UpdateAlbum(album));
            }
            catch (BusinessException e)
            {
                return new Response<Album>(e.Error);
            }
        }

        public Response<Artist> UpdateArtist(Artist artist)
        {
            try
            {
                return new Response<Artist>(dataProvider.UpdateArtist(artist));
            }
            catch (BusinessException e)
            {
                return new Response<Artist>(e.Error);
            }
        }

        public Response<Song> UpdateSong(Song song)
        {
            try
            {
                return new Response<Song>(dataProvider.UpdateSong(song));
            }
            catch (BusinessException e)
            {
                return new Response<Song>(e.Error);
            }
        }
    }
}
