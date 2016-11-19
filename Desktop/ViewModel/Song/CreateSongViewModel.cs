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
using Desktop.Data;

namespace Desktop.ViewModel
{
    class CreateSongViewModel : SongViewModel
    {

        public CreateSongViewModel(ApplicationViewModel applicationViewModel, IResponseDataProvider dataProvider, Album album) : base(applicationViewModel,dataProvider)
        {
            Model = new Song();
            model.Album = album;
            model.Number = 1;
            model.Length = new TimeSpan(0);
        }

        public ICommand Submit { get; set; }

        protected override void InitializeCommands()
        {
            base.InitializeCommands();
            Submit = new RelayCommand(o =>
            {
                ApplicationViewModel.RestClient.ExecuteAsync<Song>(ApplicationViewModel.RequestFactory.AddSongRequest(model),
                    (r, c) =>
                    {
                        if (r.Succeeded()) ApplicationViewModel.DisplayView.Execute(r.Data);
                        else ApplicationViewModel.HandlExceptionResponse(r.ExceptionResponse());
                    });
                /*Response<Song> response = DataProvider.SaveSong(model);
                if (!response.Status)
                {
                    ApplicationViewModel.HandleError(response.Error);
                } else
                ApplicationViewModel.DisplayView.Execute(response.Content);*/
            });
        }

    }
}
