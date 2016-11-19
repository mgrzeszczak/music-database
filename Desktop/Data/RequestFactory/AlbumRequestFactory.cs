using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using RestSharp;

namespace Desktop.Data
{
    class AlbumRequestFactory : IAlbumRequestFactory
    {
        public IRestRequest AddAlbumRequest(Album album)
        {
            throw new NotImplementedException();
        }

        public IRestRequest UpdateAlbumRequest(Album album)
        {
            throw new NotImplementedException();
        }

        public IRestRequest DeleteAlbumRequest(long id)
        {
            throw new NotImplementedException();
        }

        public IRestRequest GetAlbumRequest(long id)
        {
            throw new NotImplementedException();
        }

        public IRestRequest SearchAlbumsRequest(string searchText, int pageNr = 1, int amountPerPage = 10)
        {
            throw new NotImplementedException();
        }
    }
}
