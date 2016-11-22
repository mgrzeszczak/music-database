using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Common.Model;
using Common.Model.Enum;
using Common.Model.Form;
using RestSharp;

namespace Desktop.Data
{
    public class RestRequestFactory : IRestRequestFactory
    {
        private IJsonSerializer jsonSerializer = NewtonsoftJsonSerializer.Default;

        public RestRequestFactory()
        {

        }

        private IRestRequest Create<T>(string endpoint,Method method, T body)
        {
            IRestRequest request = new RestRequest(endpoint,method);
            request.JsonSerializer = jsonSerializer;
            request.AddJsonBody(body);
            return request;
        }

        private IRestRequest Authenticate(IRestRequest request)
        {
            if (LoginSession.Authentication == null) return request;
            request.AddHeader("AUTH-TOKEN", LoginSession.Authentication.Token);
            request.AddHeader("AUTH-LOGIN", LoginSession.Authentication.GetLogin());
            return request;
        }

        public IRestRequest CommonSearchRequest(string searchText, int pageNr, int amountPerPage)
        {
            var request = new RestRequest("search/all", Method.GET);
            request.AddParameter("searchText", searchText);
            request.AddParameter("pageNr", pageNr);
            request.AddParameter("amountPerPage", amountPerPage);
            return Authenticate(request);
        }

        public IRestRequest LoginRequest(string login, string password)
        {
            var request = Create("user/login", Method.POST, new {password, login});
            return request;
        }

        public IRestRequest LogoutRequest()
        {
            var request = new RestRequest("user/logout",Method.POST);
            return Authenticate(request);
        }

        public IRestRequest UserDetailsRequest()
        {
            var request = new RestRequest("user/details",Method.GET);
            return Authenticate(request);
        }

        public IRestRequest RegisterRequest(RegisterForm form)
        {
            var request = Create("user/register", Method.POST, form);
            return request;
        }

        public IRestRequest AddSongRequest(Song song)
        {
            return Authenticate(Create("song/add",Method.POST,song));
        }

        public IRestRequest UpdateSongRequest(Song song)
        {
            return Authenticate(Create("song/update", Method.PUT, song));
        }

        public IRestRequest DeleteSongRequest(long id)
        {
            var request = new RestRequest("song/delete/{id}", Method.DELETE);
            request.AddUrlSegment("id", id.ToString());
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
            var request = new RestRequest("song/fromAlbum/{id}",Method.GET);
            request.AddUrlSegment("id", albumId.ToString());
            return Authenticate(request);
        }

        public IRestRequest AddAlbumRequest(Album album)
        {
            return Authenticate(Create("album/add",Method.POST,album));
        }

        public IRestRequest UpdateAlbumRequest(Album album)
        {
            return Authenticate(Create("album/update", Method.PUT, album));
        }

        public IRestRequest DeleteAlbumRequest(long id)
        {
            var request = new RestRequest("album/delete/{id}", Method.DELETE);
            request.AddUrlSegment("id", id.ToString());
            return Authenticate(request);
        }

        public IRestRequest GetAlbumRequest(long id)
        {
            var request = new RestRequest("album/get/{id}", Method.GET);
            request.AddUrlSegment("id", id.ToString());
            return Authenticate(request);
        }
        public IRestRequest GetAlbumWithSongsRequest(long id)
        {
            var request = new RestRequest("album/getWithSongs/{id}", Method.GET);
            request.AddUrlSegment("id", id.ToString());
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
            var request = new RestRequest("album/byArtist/{id}", Method.GET);
            request.AddUrlSegment("id", artistId.ToString());
            return Authenticate(request);
        }

        public IRestRequest AddArtistRequest(Artist artist)
        {
            return Authenticate(Create("artist/add", Method.POST, artist));
        }

        public IRestRequest UpdateArtistRequest(Artist artist)
        {
            return Authenticate(Create("artist/update", Method.PUT, artist));
        }

        public IRestRequest DeleteArtistRequest(long id)
        {
            var request = new RestRequest("artist/delete/{id}", Method.DELETE);
            request.AddUrlSegment("id", id.ToString());
            return Authenticate(request);
        }

        public IRestRequest GetArtistRequest(long id)
        {
            var request = new RestRequest("artist/get/{id}", Method.GET);
            request.AddUrlSegment("id", id.ToString());
            return Authenticate(request);
        }

        public IRestRequest GetArtistWithAlbumsRequest(long id)
        {
            var request = new RestRequest("artist/getWithAlbums/{id}", Method.GET);
            request.AddUrlSegment("id", id.ToString());
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
            var request = new RestRequest("comment/get/{id}", Method.GET);
            request.AddUrlSegment("id", id.ToString());
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
            var request = new RestRequest("comment/delete/{id}", Method.DELETE);
            request.AddUrlSegment("id", id.ToString());
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
            return Authenticate(Create("rating/add", Method.POST,rating));
        }

        public IRestRequest UpdateRatingRequest(Rating rating)
        {
            return Authenticate(Create("rating/update", Method.PUT, rating));
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
