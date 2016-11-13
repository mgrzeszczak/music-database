using Common.Domain;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Communication
{
    public interface IResponseDataProvider
    {

        Response<Page<Song>> SearchSongsByTitle(string searchText, int pageNr = DataProviderDefaults.DefaultPageNr, int amountPerPage = DataProviderDefaults.DefaultAmountPerPage);
        Response<Page<Artist>> SearchArtistsByName(string searchText, int pageNr = DataProviderDefaults.DefaultPageNr, int amountPerPage = DataProviderDefaults.DefaultAmountPerPage);
        Response<Page<Album>> SearchAlbumsByName(string searchText, int pageNr = DataProviderDefaults.DefaultPageNr, int amountPerPage = DataProviderDefaults.DefaultAmountPerPage);

        Response<Artist> SaveArtist(Artist artist);
        Response<Album> SaveAlbum(Album album);
        Response<Song> SaveSong(Song song);

        Response<Song> UpdateSong(Song song);
        Response<Album> UpdateAlbum(Album album);
        Response<Artist> UpdateArtist(Artist artist);

        Response<bool> DeleteSong(long id);
        Response<bool> DeleteAlbum(long id);
        Response<bool> DeleteArtist(long id);

        Response<Song> GetSongById(long id);
        Response<Album> GetAlbumById(long id);
        Response<Artist> GetArtistById(long id);

        Response<IList<Song>> GetSongsFromAlbum(long albumId);
        Response<IList<Album>> GetAlbumsByArtist(long artistId);


    }
}
