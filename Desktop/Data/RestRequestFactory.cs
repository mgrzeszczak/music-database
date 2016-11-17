using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Common.Model;
using Common.Model.Enum;
using RestSharp;

namespace Desktop.Data
{
    class RestRequestFactory : IRestRequestFactory
    {
        private string login, token;

        public RestRequestFactory()
        {
            login = "login";
            token = "token";
        }

        public void OnLoggedIn(string login, string token)
        {
            this.login = login;
            this.token = token;
        }

        public IRestRequest Authenticate(IRestRequest request)
        {
            request.AddHeader("AUTH-TOKEN", token);
            request.AddHeader("AUTH-LOGIN", login);
            return request;
        }

        public IRestRequest LoginRequest(string login, string password)
        {
            var request = new RestRequest("user/login",Method.POST);
            request.AddJsonBody(new {password, login});
            return request;
        }

        public IRestRequest LogoutRequest()
        {
            var request = new RestRequest("user/logout",Method.POST);
            return Authenticate(request);
        }

        public IRestRequest UserDetailsRequest()
        {
            var request = new RestRequest("user/logout",Method.GET);
            return Authenticate(request);
        }

        public IRestRequest RegisterRequest(User user)
        {
            var request = new RestRequest("user/register", Method.POST);
            request.AddJsonBody(user);
            return request;
        }

        public IRestRequest AddSongRequest(Song song)
        {
            var request = new RestRequest("song/add",Method.POST);
            request.AddJsonBody(song);
            return Authenticate(request);
        }

        public IRestRequest UpdateSongRequest(Song song)
        {
            var request = new RestRequest("song/update", Method.PUT);
            request.AddJsonBody(song);
            return Authenticate(request);
        }

        public IRestRequest DeleteSongRequest(long id)
        {
            var request = new RestRequest("song/delete", Method.DELETE);
            request.AddParameter("id", id);
            return Authenticate(request);
        }

        public IRestRequest GetSongRequest(long id)
        {
            var request = new RestRequest("song/get/{id}", Method.GET);
            request.AddUrlSegment("id", id.ToString());
            return Authenticate(request);
        }

        public IRestRequest SearchSongsRequest(string searchText, int pageNr = 1, int amountPerPage = 10)
        {
            var request = new RestRequest("song/search",Method.GET);
            request.AddParameter("searchText", searchText);
            request.AddParameter("pageNr", pageNr);
            request.AddParameter("amountPerPage", amountPerPage);
            return Authenticate(request);
        }

        public IRestRequest SongsFromAlbumRequest(long albumId)
        {
            var request = new RestRequest("song/fromAlbum",Method.GET);
            request.AddParameter("albumId",albumId);
            return Authenticate(request);
        }

        public IRestRequest AddAlbumRequest(Album album)
        {
            var request = new RestRequest("album/add", Method.POST);
            request.AddJsonBody(album);
            return Authenticate(request);
        }

        public IRestRequest UpdateAlbumRequest(Album album)
        {
            var request = new RestRequest("album/update", Method.PUT);
            request.AddJsonBody(album);
            return Authenticate(request);
        }

        public IRestRequest DeleteAlbumRequest(long id)
        {
            var request = new RestRequest("album/delete", Method.DELETE);
            request.AddParameter("id", id);
            return Authenticate(request);
        }

        public IRestRequest GetAlbumRequest(long id)
        {
            var request = new RestRequest("album/get", Method.GET);
            request.AddParameter("id", id);
            return Authenticate(request);
        }

        public IRestRequest SearchAlbumsRequest(string searchText, int pageNr = 1, int amountPerPage = 10)
        {
            var request = new RestRequest("album/search", Method.GET);
            request.AddParameter("searchText",searchText);
            request.AddParameter("pageNr", pageNr);
            request.AddParameter("amountPerPage", amountPerPage);
            return Authenticate(request);
        }

        public IRestRequest AlbumsByArtistRequest(long artistId)
        {
            var request = new RestRequest("album/byArtist", Method.GET);
            request.AddParameter("artistId",artistId);
            return Authenticate(request);
        }

        public IRestRequest AddArtistRequest(Artist artist)
        {
            var request = new RestRequest("artist/add", Method.POST);
            request.AddJsonBody(artist);
            return Authenticate(request);
        }

        public IRestRequest UpdateArtistRequest(Artist artist)
        {
            var request = new RestRequest("album/update", Method.PUT);
            request.AddJsonBody(artist);
            return Authenticate(request);
        }

        public IRestRequest DeleteArtistRequest(long id)
        {
            var request = new RestRequest("artist/delete", Method.DELETE);
            request.AddParameter("id", id);
            return Authenticate(request);
        }

        public IRestRequest GetArtistRequest(long id)
        {
            var request = new RestRequest("artist/get", Method.GET);
            request.AddParameter("id", id);
            return Authenticate(request);
        }

        public IRestRequest SearchArtistsRequest(string searchText, int pageNr = 1, int amountPerPage = 10)
        {
            var request = new RestRequest("artist/search", Method.GET);
            request.AddParameter("searchText", searchText);
            request.AddParameter("pageNr", pageNr);
            request.AddParameter("amountPerPage", amountPerPage);
            return Authenticate(request);
        }

        public IRestRequest GetCommentRequest(long id)
        {
            var request = new RestRequest("comment/get", Method.GET);
            request.AddParameter("id", id);
            return Authenticate(request);
        }

        public IRestRequest AddCommentRequest(Comment comment)
        {
            var request = new RestRequest("comment/add", Method.POST);
            request.AddJsonBody(comment);
            return Authenticate(request);
        }

        public IRestRequest FindCommentsRequest(EntityType entityType, long entityId, int pageNr = 1, int amountPerPage = 10)
        {
            var request = new RestRequest("comment/find", Method.GET);
            request.AddParameter("entityType", entityType);
            request.AddParameter("entityId", entityId);
            request.AddParameter("pageNr", pageNr);
            request.AddParameter("amountPerPage", amountPerPage);
            return Authenticate(request);
        }

        public IRestRequest DeleteCommentRequest(long id)
        {
            var request = new RestRequest("comment/delete", Method.DELETE);
            request.AddParameter("id", id);
            return Authenticate(request);
        }

        public IRestRequest SearchCommentsRequest(string searchText, int pageNr = 1, int amountPerPage = 10)
        {
            var request = new RestRequest("comment/search", Method.GET);
            request.AddParameter("searchText", searchText);
            request.AddParameter("pageNr", pageNr);
            request.AddParameter("amountPerPage", amountPerPage);
            return Authenticate(request);
        }

        public IRestRequest AddRatingRequest(Rating rating)
        {
            var request = new RestRequest("rating/add", Method.POST);
            request.AddJsonBody(rating);
            return Authenticate(request);
        }

        public IRestRequest UpdateRatingRequest(Rating rating)
        {
            var request = new RestRequest("rating/update", Method.PUT);
            request.AddJsonBody(rating);
            return Authenticate(request);
        }

        public IRestRequest AverageRatingRequest(EntityType entityType, long entityId)
        {
            var request = new RestRequest("rating/average", Method.GET);
            request.AddParameter("entityType", entityType);
            request.AddParameter("entityId", entityId);
            return Authenticate(request);
        }

        public IRestRequest GetRatingRequest(EntityType entityType, long entityId, long userId)
        {
            var request = new RestRequest("rating/get", Method.GET);
            request.AddParameter("entityType", entityType);
            request.AddParameter("entityId", entityId);
            request.AddParameter("userId", userId);
            return Authenticate(request);
        }
    }
}
