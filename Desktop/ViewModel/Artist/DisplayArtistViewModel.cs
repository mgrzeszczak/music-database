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
using Common.Message;
using Common.Model.Enum;
using Desktop.Data;

namespace Desktop.ViewModel
{
    class DisplayArtistViewModel : ArtistViewModel, ICacheable
    {

        public DisplayArtistViewModel(ApplicationViewModel applicationViewModel, IResponseDataProvider dataProvider, long artistId) : base(applicationViewModel, dataProvider)
        {
            RestClient.ExecuteAsync<ArtistWithAlbums>(RequestFactory.GetArtistWithAlbumsRequest(artistId),
                    (r, c) =>
                    {
                        if (r.Succeeded())
                        {
                            Albums = new ObservableCollection<Album>(r.Data.Albums);
                            Model = r.Data.Artist;

                            Console.WriteLine(Model.Name);

                        }
                        else ApplicationViewModel.HandlExceptionResponse(r.ExceptionResponse());
                    });


            RefreshComments(artistId, 1);

            ApplicationViewModel.RestClient.ExecuteAsync<Rating>(
                RequestFactory.GetRatingRequest(EntityType.ARTIST, artistId, LoginSession.Authentication.User.Id),
                (resp, handle) =>
                {
                    if (resp.Succeeded())
                    {
                        rating = resp.Data;
                        UserRating = resp.Data == null ? 0 : resp.Data.Value;
                    }
                    else
                    {
                        resp.ExceptionResponse();
                        ApplicationViewModel.HandlExceptionResponse(resp.ExceptionResponse());
                    }
                });

            RestClient.ExecuteAsync<double>(RequestFactory.AverageRatingRequest(EntityType.ARTIST, artistId),
                (resp, handle) =>
                {
                    if (resp.Succeeded()) averageRating = resp.Data;
                    else;
                });
            /*Response<Artist> respArtist = DataProvider.GetArtistById(artistId);
            Response<IList<Album>> respAlbums = DataProvider.GetAlbumsByArtist(artistId);
            if (!respArtist.Status || !respAlbums.Status)
            {
                ApplicationViewModel.HandleError(!respArtist.Status ? respArtist.Error : respAlbums.Error);
                return;
            }
            Albums = new ObservableCollection<Album>(respAlbums.Content);
            Model = respArtist.Content;*/
        }

        private void UpdateUserRating()
        {
            if (rating != null) rating.Value = userRating;
            if (userRating < 1 || userRating > 5) return;
            RestClient.ExecuteAsync<Rating>(
                rating == null ?
                RequestFactory.AddRatingRequest(new Rating()
                {
                    EntityType = EntityType.ARTIST,
                    EntityId = Model.Id,
                    Value = UserRating,
                    User = LoginSession.Authentication.User
                }) : RequestFactory.UpdateRatingRequest(rating),
                (resp, handle) =>
                {
                    if (resp.Succeeded())
                    {
                        rating = resp.Data;
                        var resp2 = RestClient.Execute<double>(RequestFactory.AverageRatingRequest(EntityType.ARTIST, Model.Id));
                        if (resp2.Succeeded()) AverageRating = resp2.Data;
                        else ApplicationViewModel.HandlExceptionResponse(resp2.ExceptionResponse());
                    }
                    else ApplicationViewModel.HandlExceptionResponse(resp.ExceptionResponse());
                });
        }

        private void RefreshComments(long modelId, int pageNumber)
        {
            RestClient.ExecuteAsync<Page<Comment>>(RequestFactory.FindCommentsRequest(EntityType.ARTIST, modelId, pageNumber),
                (resp, handle) =>
                {
                    if (resp.Succeeded())
                    {
                        CommentPage = resp.Data;
                        foreach (var c in CommentPage.Items) Console.WriteLine(c.Content);
                    }
                    else ApplicationViewModel.HandlExceptionResponse(resp.ExceptionResponse());
                });
        }

        public ICommand PostComment { get; set; }
        public ICommand NextCommentPage { get; set; }
        public ICommand PreviousCommentPage { get; set; }

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

        public ICommand Delete { get; set; }
        public ICommand CreateAlbum { get; set; }
        public ICommand AdminDeleteComment { get; set; }

        private ObservableCollection<Album> albums;
        public ObservableCollection<Album> Albums
        {
            get
            {
                return albums;
            }
            set
            {
                albums = value;
                OnPropertyChanged(nameof(Albums));
            }
        }

        public ViewModel GetViewModel()
        {
            return new DisplayArtistViewModel(ApplicationViewModel, DataProvider, model.Id);
        }

        protected override void InitializeCommands()
        {
            base.InitializeCommands();
            Delete = new RelayCommand(o =>
            {
                ApplicationViewModel.RestClient.ExecuteAsync<Artist>(ApplicationViewModel.RequestFactory.DeleteArtistRequest(model.Id),
                    (r, c) =>
                    {
                        if (r.Succeeded())
                        {
                            ApplicationViewModel.Search.Execute(model.Name);
                            ApplicationViewModel.ClearHistory();
                        }
                        else ApplicationViewModel.HandlExceptionResponse(r.ExceptionResponse());
                    });
                /*
                Response<bool> response = DataProvider.DeleteArtist(model.Id);
                if (!response.Status) ApplicationViewModel.HandleError(response.Error);
                else
                {
                    ApplicationViewModel.Search.Execute(model.Name);
                    ApplicationViewModel.ClearHistory();
                }*/
            });
            CreateAlbum = new RelayCommand(o =>
            {
                ApplicationViewModel.CreateView.Execute(new CreateAlbumViewModel(ApplicationViewModel, DataProvider, model));
            });

            NextCommentPage = new RelayCommand(o => RefreshComments(Model.Id, CommentPage.PageNumber + 1));
            PreviousCommentPage = new RelayCommand(o => RefreshComments(Model.Id, CommentPage.PageNumber - 1));
            PostComment = new RelayCommand(o =>
            {
                Comment comment = new Comment()
                {
                    EntityType = EntityType.ARTIST,
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
                    if (resp.Succeeded()) RefreshComments(Model.Id, CommentPage.PageNumber);
                    else ApplicationViewModel.HandlExceptionResponse(resp.ExceptionResponse());
                });
            });
        }
    }
}
