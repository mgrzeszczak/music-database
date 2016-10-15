using Common.Model;
using Common.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Service
{
    public class SongService
    {
        private SongRepository songRepository;

        public SongService(SongRepository songRepository)
        {
            this.songRepository = songRepository;
        }

        public void test()
        {
            Song song = new Song();
            song.Title = "not gonna be in that shite";
            songRepository.save(song);
            Console.WriteLine(song.Id);
           // throw new Exception("blarg");
        }

    }
}
