using Common.Model;
using System.Collections.Generic;

namespace Common.Communication
{
    public interface IDataProvider
    {
        IList<Song> SearchSongsByTitle(string searchText, int pageNr=DataProviderDefaults.DefaultPageNr, int amountPerPage = DataProviderDefaults.DefaultAmountPerPage);
        IList<Artist> SearchArtistsByName(string searchText, int pageNr = DataProviderDefaults.DefaultPageNr, int amountPerPage = DataProviderDefaults.DefaultAmountPerPage);
        IList<Album> SearchAlbumsByName(string searchText, int pageNr = DataProviderDefaults.DefaultPageNr, int amountPerPage=DataProviderDefaults.DefaultAmountPerPage);

        Song SaveSong(Song song);
        Artist SaveArtist(Artist artist);
        Album SaveAlbum(Album album);

        void UpdateSong(Song song);
        void UpdateAlbum(Album album);
        void UpdateArtist(Artist artist);

        void DeleteSong(Song song);
        void DeleteAlbum(Album album);
        void DeleteArtist(Artist artist);
    }
}
