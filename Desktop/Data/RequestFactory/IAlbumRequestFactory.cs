using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using RestSharp;

namespace Desktop.Data
{
    interface IAlbumRequestFactory
    {
        IRestRequest AddAlbumRequest(Album album);
        IRestRequest UpdateAlbumRequest(Album album);
        IRestRequest DeleteAlbumRequest(long id);
        IRestRequest GetAlbumRequest(long id);
        IRestRequest SearchAlbumsRequest(string searchText, int pageNr = 1, int amountPerPage = 10);
    }
}
