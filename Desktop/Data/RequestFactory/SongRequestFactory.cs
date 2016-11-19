using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using RestSharp;

namespace Desktop.Data
{
    class SongRequestFactory : ISongRequestFactory
    {
        public IRestRequest AddSongRequest(Song song)
        {
            throw new NotImplementedException();
        }

        public IRestRequest UpdateSongRequest(Song song)
        {
            throw new NotImplementedException();
        }

        public IRestRequest DeleteSongRequest(long id)
        {
            throw new NotImplementedException();
        }

        public IRestRequest GetSongRequest(long id)
        {
            throw new NotImplementedException();
        }

        public IRestRequest SearchSongsRequest(string searchText, int pageNr = 1, int amountPerPage = 10)
        {
            throw new NotImplementedException();
        }

        public IRestRequest SongsFromAlbumRequest(long albumId)
        {
            throw new NotImplementedException();
        }
    }
}
