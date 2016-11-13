using Common.Domain;
using Common.Model;
using System.Collections.Generic;

namespace Common.Communication
{
    public interface IDataProvider
    {
        Page<Song> SearchSongsByTitle(string searchText, int pageNr=DataProviderDefaults.DefaultPageNr, int amountPerPage = DataProviderDefaults.DefaultAmountPerPage);
        Page<Artist> SearchArtistsByName(string searchText, int pageNr = DataProviderDefaults.DefaultPageNr, int amountPerPage = DataProviderDefaults.DefaultAmountPerPage);
        Page<Album> SearchAlbumsByName(string searchText, int pageNr = DataProviderDefaults.DefaultPageNr, int amountPerPage=DataProviderDefaults.DefaultAmountPerPage);

        Artist SaveArtist(Artist artist);
        Album SaveAlbum(Album album);
        Song SaveSong(Song song);
        
        Song UpdateSong(Song song);
        Album UpdateAlbum(Album album);
        Artist UpdateArtist(Artist artist);

        void DeleteSong(long id);
        void DeleteAlbum(long id);
        void DeleteArtist(long id);

        Song GetSongById(long id);
        Album GetAlbumById(long id);
        Artist GetArtistById(long id);

        IList<Song> GetSongsFromAlbum(long albumId);
        IList<Album> GetAlbumsByArtist(long artistId);

    }
}
