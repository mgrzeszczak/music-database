using Backend.Communication;
using Common.Communication;
using Common.Domain;
using Common.Model;
using Desktop.Cache;
using Desktop.Command;
using System;
using System.Windows;
using System.Windows.Input;
using Common.Exception;
using Desktop.Data;
using RestSharp;

namespace Desktop.ViewModel
{
    class ApplicationViewModel : ViewModel
    {
        private IResponseDataProvider dataProvider;
        public IRestRequestFactory RequestFactory { get; }
        public IRestClient RestClient { get; }

        public ApplicationViewModel()
        {
            RequestFactory = new RestRequestFactory();
            RestClient = new RestClient("http://164.132.63.49/musicdb/api");
            var jsonSerializer = NewtonsoftJsonSerializer.Default;
            RestClient.AddHandler("application/json", jsonSerializer);
            RestClient.AddHandler("text/json", jsonSerializer);
            RestClient.AddHandler("text/x-json", jsonSerializer);
            RestClient.AddHandler("text/javascript", jsonSerializer);
            RestClient.AddHandler("*+json", jsonSerializer);

            History = new BrowsingHistory(this);
            //dataProvider = new BackendResponseDataProvider();
            //CurrentViewModel = new HomeViewModel(this, dataProvider);
            CurrentViewModel = new LoginViewModel(this,dataProvider);
        }

        public ICommand PreviousPage { get; set; }
        public ICommand DisplayView { get; set; }
        public ICommand EditView { get; set; }
        public ICommand CreateView { get; set; }

        public ICommand Home { get; set; }
        public ICommand Search { get; set; }

        public ICommand LoginPage { get; set; }
        public ICommand RegisterPage { get; set; }

        protected override void InitializeCommands()
        {
            DisplayView = new RelayCommand((o) =>
            {                 
                if (o is Song)
                    ExecuteChangePage(new DisplaySongViewModel(this,dataProvider,(o as Song).Id));
                else if (o is Album)
                    ExecuteChangePage(new DisplayAlbumViewModel(this, dataProvider, (o as Album).Id));
                else if (o is Artist)
                    ExecuteChangePage(new DisplayArtistViewModel(this, dataProvider, (o as Artist).Id));
            });
            EditView = new RelayCommand((o) =>
            {
                if (o is Song)
                    ExecuteChangePage(new EditSongViewModel(this, dataProvider, (o as Song).Id));
                else if (o is Album)
                    ExecuteChangePage(new EditAlbumViewModel(this, dataProvider, (o as Album).Id));
                else if (o is Artist)
                    ExecuteChangePage(new EditArtistViewModel(this, dataProvider, (o as Artist).Id));
            });
            CreateView = new RelayCommand((o) =>
            {
                if (o is Album)
                    ExecuteChangePage(new CreateSongViewModel(this, dataProvider, (o as Album)));
                else if (o is Artist)
                    ExecuteChangePage(new CreateAlbumViewModel(this, dataProvider, (o as Artist)));
                else if (o == null)
                    ExecuteChangePage(new CreateArtistViewModel(this, dataProvider));
            });

            PreviousPage = new RelayCommand(o => ExecutePreviousPage());

            Home = new RelayCommand(o => ExecuteChangePage(new HomeViewModel(this,dataProvider)));
            Search = new RelayCommand(o => ExecuteChangePage(new SearchViewModel(this,dataProvider,o as string)));
            LoginPage = new RelayCommand(o=>ExecuteChangePage(new LoginViewModel(this,dataProvider)));
            RegisterPage = new RelayCommand(o => ExecuteChangePage(new RegisterViewModel(this, dataProvider)));
        }

        public void HandlExceptionResponse(ExceptionResponse ex)
        {
            
            Console.WriteLine();
            Console.WriteLine(ex.ErrorCode);
            Console.WriteLine(ex.Message);
            foreach (var e in ex.Errors) Console.WriteLine(e.Key+ " " +e.Value);
            Console.WriteLine();

            switch (ex.ErrorCode)
            {
                case Error.ARTIST_NAME_TAKEN:
                    MessageBox.Show("Arist with that name already exists.", "Error");
                    break;
                case Error.ALBUM_NAME_TAKEN:
                    MessageBox.Show("There already is an album with that name for that artist.", "Error");
                    break;
                case Error.SONG_TITLE_TAKEN:
                    MessageBox.Show("There already is a song with that title for that album.", "Error");
                    break;
                case Error.SONG_NUMBER_TAKEN:
                    MessageBox.Show("There already is a song with that number for that album.", "Error");
                    break;
                case Error.ALBUM_NUMBER_TAKEN:
                    MessageBox.Show("There already is an album with that number for that artist.", "Error");
                    break;
                case Error.UNKNOWN_ERROR:
                    MessageBox.Show("Unknown error occured.", "Error");
                    ExecuteChangePage(new LoginViewModel(this, dataProvider));
                    break;
                case Error.VALIDATION_FAILED:
                    MessageBox.Show("Email or password is invalid. Password must be at least 8 characters long, have at least 1 uppercase and lowercase character, at least one digit and at least one special character (!@#$%^&*).", "Error");
                    break;
                case Error.LOGIN_TAKEN:
                    MessageBox.Show("Login taken", "Error");
                    break;
                case Error.INVALID_VERSION:
                    MessageBox.Show("Object was edited in the meantime", "Error");
                    if (CurrentViewModel is EditSongViewModel)
                    {
                        ExecuteChangePage(new DisplaySongViewModel(this,dataProvider,(CurrentViewModel as DisplaySongViewModel).Model.Id));
                    } else if (CurrentViewModel is EditAlbumViewModel)
                    {
                        ExecuteChangePage(new DisplayAlbumViewModel(this, dataProvider, (CurrentViewModel as DisplayAlbumViewModel).Model.Id));
                    } else if (CurrentViewModel is EditArtistViewModel)
                    {
                        ExecuteChangePage(new DisplayArtistViewModel(this, dataProvider, (CurrentViewModel as DisplayArtistViewModel).Model.Id));
                    }
                    break;
                case Error.INVALID_CREDENTIALS:
                    MessageBox.Show("Invalid login or password", "Error");
                    break;
                case Error.UNAUTHORIZED:
                    MessageBox.Show("You've been logged out.", "Error");
                    ExecuteChangePage(new LoginViewModel(this, dataProvider));
                    break;
            }
        }

        public void HandleError(Error error)
        {
            switch (error)
            {
                case Error.ARTIST_NAME_TAKEN:
                    MessageBox.Show("Arist with that name already exists.","Error");
                    break;
                case Error.ALBUM_NAME_TAKEN:
                    MessageBox.Show("There already is an album with that name for that artist.", "Error");
                    break;
                case Error.SONG_TITLE_TAKEN:
                    MessageBox.Show("There already is a song with that title for that album.", "Error");
                    break;
                case Error.SONG_NUMBER_TAKEN:
                    MessageBox.Show("There already is a song with that number for that album.", "Error");
                    break;
                case Error.ALBUM_NUMBER_TAKEN:
                    MessageBox.Show("There already is an album with that number for that artist.", "Error");
                    break;
                case Error.UNKNOWN_ERROR:
                    MessageBox.Show("Unknown error occured.", "Error");
                    ExecuteChangePage(DefaultPage());
                    break;
                case Error.VALIDATION_FAILED:
                    break;
                case Error.UNAUTHORIZED:
                    MessageBox.Show("You've been logged out.", "Error");
                    ExecuteChangePage(new LoginViewModel(this,dataProvider));
                    break;
            }
        }

        private void ExecuteChangePage(ViewModel page)
        {
            History.Add(CurrentViewModel);
            CurrentViewModel = page;
        }

        private void ExecutePreviousPage()
        {
            CurrentViewModel = History.GetPreviousPage();
        }

        private IBrowsingHistory History { get; set; }

        private ViewModel currentViewModel;
        public ViewModel CurrentViewModel
        {
            get { return currentViewModel; }
            private set
            {
                currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public ViewModel DefaultPage()
        {
            return new HomeViewModel(this, dataProvider);
        }

        public void ClearHistory()
        {
            History.Clear();
        }
    }
}
