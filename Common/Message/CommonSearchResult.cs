using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;

namespace Common.Domain
{
    public class CommonSearchResult
    {
        public readonly Page<Song> songResult;
        public readonly Page<Album> albumResult;
        public readonly Page<Artist> artistResult;

        public CommonSearchResult(Page<Song> songResult, Page<Album> albumResult, Page<Artist> artistResult)
        {
            this.songResult = songResult;
            this.albumResult = albumResult;
            this.artistResult = artistResult;
        }
    }
}
