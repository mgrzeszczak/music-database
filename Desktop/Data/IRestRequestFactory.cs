using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using Common.Model.Enum;
using Common.Model.Form;
using RestSharp;

namespace Desktop.Data
{
    public interface IRestRequestFactory
    {
        IRestRequest CommonSearchRequest(string searchText, int pageNr, int amountPerPage);

        // USER CONTROLLER
        IRestRequest LoginRequest(string login, string password);
        IRestRequest LogoutRequest();
        IRestRequest UserDetailsRequest();
        IRestRequest RegisterRequest(RegisterForm form);

        // SONG CONTROLLER
        IRestRequest AddSongRequest(Song song);
        IRestRequest UpdateSongRequest(Song song);
        IRestRequest DeleteSongRequest(long id);
        IRestRequest GetSongRequest(long id);
        IRestRequest SearchSongsRequest(string searchText, int pageNr=1,int amountPerPage=10);
        IRestRequest SongsFromAlbumRequest(long albumId);

        // ALBUM CONTROLLER
        IRestRequest AddAlbumRequest(Album album);
        IRestRequest UpdateAlbumRequest(Album album);
        IRestRequest DeleteAlbumRequest(long id);
        IRestRequest GetAlbumRequest(long id);
        IRestRequest GetAlbumWithSongsRequest(long id);
        IRestRequest SearchAlbumsRequest(string searchText, int pageNr = 1, int amountPerPage = 10);
        IRestRequest AlbumsByArtistRequest(long artistId);

        // ARTIST CONTROLLER
        IRestRequest AddArtistRequest(Artist artist);
        IRestRequest UpdateArtistRequest(Artist artist);
        IRestRequest DeleteArtistRequest(long id);
        IRestRequest GetArtistRequest(long id);
        IRestRequest GetArtistWithAlbumsRequest(long id);
        IRestRequest SearchArtistsRequest(string searchText, int pageNr = 1, int amountPerPage = 10);

        // COMMENT CONTROLLER
        IRestRequest GetCommentRequest(long id);
        IRestRequest AddCommentRequest(Comment comment);
        IRestRequest FindCommentsRequest(EntityType entityType, long entityId, int pageNr = 1, int amountPerPage = 10);
        IRestRequest DeleteCommentRequest(long id);
        IRestRequest SearchCommentsRequest(string searchText, int pageNr = 1, int amountPerPage = 10);

        // RATING CONTROLLER
        IRestRequest AddRatingRequest(Rating rating);
        IRestRequest UpdateRatingRequest(Rating rating);
        IRestRequest AverageRatingRequest(EntityType entityType,long entityId);
        IRestRequest GetRatingRequest(EntityType entityType, long entityId, long userId);

    }
}
