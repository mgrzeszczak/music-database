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

                ApplicationViewModel.RestClient.ExecuteAsync<Album>(ApplicationViewModel.RequestFactory.AddAlbumRequest(model),
                    (r, c) =>
                    {
                        if (r.Succeeded()) ApplicationViewModel.DisplayView.Execute(r.Data);
                        else ApplicationViewModel.HandlExceptionResponse(r.ExceptionResponse());
                    });

                /*Response<Album> response = DataProvider.SaveAlbum(model);
                Console.WriteLine(response.Status);
                Console.WriteLine(response.Content);
                if (!response.Status)
                {
                    ApplicationViewModel.HandleError(response.Error);
                } else ApplicationViewModel.DisplayView.Execute(response.Content);*/
            });
        }
    }
}
