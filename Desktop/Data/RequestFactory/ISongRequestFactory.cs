using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using RestSharp;

namespace Desktop.Data
{
    interface ISongRequestFactory
    {
        IRestRequest AddSongRequest(Song song);
        IRestRequest UpdateSongRequest(Song song);
        IRestRequest DeleteSongRequest(long id);
        IRestRequest GetSongRequest(long id);
        IRestRequest SearchSongsRequest(string searchText, int pageNr = 1, int amountPerPage = 10);
        IRestRequest SongsFromAlbumRequest(long albumId);
    }
}
