using Common.Communication;
using Common.Domain;
using Common.Model;
using Desktop.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Desktop.ViewModel
{
    class CreateAlbumViewModel : AlbumViewModel
    {
        public CreateAlbumViewModel(ApplicationViewModel applicationViewModel, IResponseDataProvider dataProvider, Artist artist) : base(applicationViewModel,dataProvider)
        {
            Model = new Album();
            model.Artist = artist;
            model.Year = 1900;
            model.Length = new TimeSpan(0);
            model.Number = 1;
        }

        public ICommand Submit { get; set; }

        protected override void InitializeCommands()
        {
            base.InitializeCommands();
            Submit = new RelayCommand(o => {
                Response<Album> response = DataProvider.SaveAlbum(model);
                Console.WriteLine(response.Status);
                Console.WriteLine(response.Content);
                if (!response.Status)
                {
                    ApplicationViewModel.HandleError(response.Error);
                } else ApplicationViewModel.DisplayView.Execute(response.Content);
            });
        }
    }
}
