using Common.Communication;
using Common.Domain;
using Common.Model;
using Desktop.Cache;
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
    class EditAlbumViewModel : AlbumViewModel
    {
        public EditAlbumViewModel(ApplicationViewModel applicationViewModel, IResponseDataProvider dataProvider, long albumId) : base(applicationViewModel,dataProvider)
        {
            ApplicationViewModel.RestClient.ExecuteAsync<Album>(ApplicationViewModel.RequestFactory.GetAlbumRequest(albumId),
                    (r, c) =>
                    {
                        if (r.Succeeded()) Model = r.Data;
                        else ApplicationViewModel.HandlExceptionResponse(r.ExceptionResponse());
                    });
            /*Response<Album> respAlbum = DataProvider.GetAlbumById(albumId);
            if (!respAlbum.Status)
            {
                ApplicationViewModel.HandleError(respAlbum.Error);
                return;
            }
            Model = respAlbum.Content;*/
        }

        protected override void InitializeCommands()
        {
            Submit = new RelayCommand(o => {
                ApplicationViewModel.RestClient.ExecuteAsync<Album>(ApplicationViewModel.RequestFactory.UpdateAlbumRequest(model),
                    (r, c) =>
                    {
                        if (r.Succeeded()) ApplicationViewModel.DisplayView.Execute(r.Data);
                        else ApplicationViewModel.HandlExceptionResponse(r.ExceptionResponse());
                    });
                /*Response<Album> response = DataProvider.UpdateAlbum(model);
                if (!response.Status)
                {
                    ApplicationViewModel.HandleError(response.Error);
                }
                else ApplicationViewModel.DisplayView.Execute(response.Content);*/
            });
            base.InitializeCommands();
        }

        public ICommand Submit { get; set; }

    }
}
