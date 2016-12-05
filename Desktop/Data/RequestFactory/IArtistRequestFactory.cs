using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using RestSharp;

namespace Desktop.Data
{
    interface IArtistRequestFactory
    {
        IRestRequest AddArtistRequest(Artist artist);
        IRestRequest UpdateArtistRequest(Artist artist);
        IRestRequest DeleteArtistRequest(long id);
        IRestRequest GetArtistRequest(long id);
        IRestRequest SearchArtistsRequest(string searchText, int pageNr = 1, int amountPerPage = 10);
    }
}
