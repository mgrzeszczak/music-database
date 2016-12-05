 using Common.Communication;
using Common.Domain;
using Common.Model;
using Desktop.Cache;
using Desktop.Command;
using System;
using System.Collections.Generic;
 using System.Collections.ObjectModel;
 using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
 using Common.Model.Enum;
 using Desktop.Data;
 using RestSharp;

namespace Desktop.ViewModel
{
    class DisplaySongViewModel : SongViewModel, ICacheable
    {

        public DisplaySongViewModel(ApplicationViewModel applicationViewModel, IResponseDataProvider dataProvider, long songId) : base(applicationViewModel,dataProvider)
        {
            /*Response<Song> response = DataProvider.GetSongById(songId);
            if (!response.Status)
            {
                ApplicationViewModel.HandleError(response.Error);
                return;
            }
            Model = response.Content;*/
            ApplicationViewModel.RestClient.ExecuteAsync<Song>(
                ApplicationViewModel.RequestFactory.GetSongRequest(songId),
                (resp,handle) =>
                {
                    if (resp.Succeeded()) Model = resp.Data;
                    else ApplicationViewModel.HandlExceptionResponse(resp.ExceptionResponse());
                });
            RefreshComments(songId,1);

            ApplicationViewModel.RestClient.ExecuteAsync<Rating>(
                RequestFactory.GetRatingRequest(EntityType.SONG, songId, LoginSession.Authentication.User.Id),
                (resp, handle) =>
                {
                    if (resp.Succeeded())
                    {
                        rating = resp.Data;
                        UserRating = resp.Data==null? 0 : resp.Data.Value;
                    }
                    else
                    {
                        resp.ExceptionResponse();
                        ApplicationViewModel.HandlExceptionResponse(resp.ExceptionResponse());
                    }
                });

            RestClient.ExecuteAsync<double>(RequestFactory.AverageRatingRequest(EntityType.SONG, songId),
                (resp, handle) =>
                {
                    if (resp.Succeeded()) averageRating = resp.Data;
                    else ;
                });
        }

        private Rating rating = null;

        private int userRating;
        public int UserRating
        {
            get { return userRating; }
            set
            {
                userRating = value;
                OnPropertyChanged(nameof(UserRating));
                UpdateUserRating();
            }
        }

        private double averageRating;
        public double AverageRating
        {
            get { return averageRating; }
            set
            {
                averageRating = value;
                OnPropertyChanged(nameof(AverageRating));
            }
        }

        private Page<Comment> commentPage;
        public Page<Comment> CommentPage
        {
            get
            {
                return commentPage;
            }
            set
            {
                commentPage = value;
                OnPropertyChanged(nameof(CommentPage));
            }
        }

        private string commentContent;
        public string CommentContent
        {
            get
            {
                return commentContent;
            }
            set
            {
                commentContent = value;
                OnPropertyChanged(nameof(CommentContent));
            }
        }

        private bool isClosing;
        public bool IsClosing
        {
            get
            {
                return isClosing;
            }
            set
            {
                isClosing = value;
                OnPropertyChanged(nameof(IsClosing));
            }
        }

        

        
        public ICommand Home { get; set; }
        public ICommand Back { get; set; }
        public ICommand Edit { get; set; }
        public ICommand Delete { get; set; }

        public ICommand AdminDeleteComment { get; set; }

        public ICommand PostComment { get; set; }
        public ICommand NextCommentPage { get; set; }
        public ICommand PreviousCommentPage { get; set; }

        public ViewModel GetViewModel()
        {
            return new DisplaySongViewModel(ApplicationViewModel, DataProvider, model.Id);
        }

        private void UpdateUserRating()
        {
            if (rating != null) rating.Value = userRating;
            RestClient.ExecuteAsync<Rating>(
                rating == null? 
                RequestFactory.AddRatingRequest(new Rating()
                {
                    EntityType = EntityType.SONG,
                    EntityId = Model.Id,
                    Value = UserRating,
                    User = LoginSession.Authentication.User
                }) : RequestFactory.UpdateRatingRequest(rating),
                (resp, handle) =>
                {
                    if (resp.Succeeded())
                    {
                        rating = resp.Data;
                        var resp2 = RestClient.Execute<double>(RequestFactory.AverageRatingRequest(EntityType.SONG, Model.Id));
                        if (resp2.Succeeded()) AverageRating = resp2.Data;
                        else ApplicationViewModel.HandlExceptionResponse(resp2.ExceptionResponse());
                    } else ApplicationViewModel.HandlExceptionResponse(resp.ExceptionResponse());
                });
        }

        private void RefreshComments(long modelId, int pageNumber)
        {
            RestClient.ExecuteAsync<Page<Comment>>(RequestFactory.FindCommentsRequest(EntityType.SONG, modelId, pageNumber),
                (resp, handle) =>
                {
                    if (resp.Succeeded())
                    {
                        CommentPage = resp.Data;
                        foreach( var c in CommentPage.Items) Console.WriteLine(c.Content);
                    }
                    else ApplicationViewModel.HandlExceptionResponse(resp.ExceptionResponse());
                });
        }

        protected override void InitializeCommands()
        {
            base.InitializeCommands();
            
            Delete = new RelayCommand(o =>
            {
                IsClosing = true;


                ApplicationViewModel.RestClient.ExecuteAsync<Artist>(ApplicationViewModel.RequestFactory.DeleteSongRequest(model.Id),
                    (r, c) =>
                    {
                        if (r.Succeeded())
                        {
                            ApplicationViewModel.DisplayView.Execute(model.Album);
                            ApplicationViewModel.ClearHistory();
                        }
                        else ApplicationViewModel.HandlExceptionResponse(r.ExceptionResponse());
                    });
                /*Response<bool> response = DataProvider.DeleteSong(model.Id);
                if (!response.Status) ApplicationViewModel.HandleError(response.Error);
                else
                {
                    ApplicationViewModel.DisplayView.Execute(model.Album);
                    ApplicationViewModel.ClearHistory();
                }*/
            });
            Home = new RelayCommand(o =>
            {
                IsClosing = true;
                ApplicationViewModel.Home.Execute(null);
            });
            Back = new RelayCommand(o =>
            {
                IsClosing = true;
                ApplicationViewModel.PreviousPage.Execute(null);
            });
            Edit = new RelayCommand(o =>
            {
                IsClosing = true;
                ApplicationViewModel.EditView.Execute(Model);
            });

            NextCommentPage = new RelayCommand(o => RefreshComments(Model.Id, CommentPage.PageNumber + 1));
            PreviousCommentPage = new RelayCommand(o => RefreshComments(Model.Id, CommentPage.PageNumber - 1));
            PostComment = new RelayCommand(o =>
            {
                Comment comment = new Comment()
                {
                    EntityType = EntityType.SONG,
                    User = LoginSession.Authentication.User,
                    EntityId = Model.Id,
                    Content = CommentContent
                };
                RestClient.ExecuteAsync<Comment>(RequestFactory.AddCommentRequest(comment), (resp, handle) =>
                {
                    if (resp.Succeeded()) RefreshComments(Model.Id, 1);
                    else ApplicationViewModel.HandlExceptionResponse(resp.ExceptionResponse());
                });
            });

            AdminDeleteComment = new RelayCommand(o =>
            {
                if (!(o is Comment)) return;
                Comment comment = o as Comment;
                RestClient.ExecuteAsync(RequestFactory.DeleteCommentRequest(comment.Id), (resp, handle) =>
                {
                    if (resp.Succeeded()) RefreshComments(Model.Id,CommentPage.PageNumber);
                    else ApplicationViewModel.HandlExceptionResponse(resp.ExceptionResponse());
                });
            });

        }

    }
}
