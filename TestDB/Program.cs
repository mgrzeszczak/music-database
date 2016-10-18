using Backend.Communication;
using Common.Communication;
using Common.Model;

namespace TestDB
{
    class Program
    {
        static void Main(string[] args)
        {
            IDataProvider backendDataProvider = new BackendDataProvider();
            
            Song song = new Song();
            song.Title = "You Don't Fool Me";
            backendDataProvider.SaveSong(song);
        }
    }
}
