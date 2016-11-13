using Common.Communication;
using Common.Domain;
using Common.Model;
using Desktop.Cache;
using Desktop.Command;
using Desktop.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Desktop.ViewModel
{
    public enum SearchFocus
    {
        Songs,
        Albums,
        Artists,
        Any
    }

    class SearchViewModel : ChildViewModel, ICacheable
    {

        public SearchViewModel(ApplicationViewModel applicationViewModel, IResponseDataProvider dataProvider, string searchText) : base(applicationViewModel, dataProvider)
        {
            SearchText = searchText;
            SearchByText(searchText);
        }

        public ViewModel GetViewModel()
        {
            return new SearchViewModel(ApplicationViewModel, DataProvider, SearchText);
        }

        private Page<Artist> artistPage;
        private Page<Song> songPage;
        private Page<Album> albumPage;

        public ICommand DelayedSearch { get; set; }

        public ICommand NextResultPage { get; set; }
        public ICommand PreviousResultPage { get; set; }
        
        public ICommand SetSearchFocus { get; set; }

        public ICommand CreateArtist { get; set; }

        protected override void InitializeCommands()
        {
            base.InitializeCommands();
            CreateArtist = new RelayCommand((o) => {
                Console.WriteLine("CreateArtistButton");
                ApplicationViewModel.CreateView.Execute(new CreateArtistViewModel(ApplicationViewModel, DataProvider));
                });

            DelayedSearch = new RelayCommand((o) => (o as string) != null && (o as string).Length > 0, (o) => SearchByText(o as string));
            
            NextResultPage = new RelayCommand(o => (songPage!=null&&songPage.TotalPages>Page) 
                                                    || (albumPage != null && albumPage.TotalPages > Page) 
                                                    || (artistPage != null && artistPage.TotalPages > Page), o =>
            {
                Console.WriteLine("Next result page - current page: " + Page);
                if (SearchFocus == SearchFocus.Any) return;
                switch (SearchFocus)
                {
                    case SearchFocus.Albums:
                        if (Page < albumPage.TotalPages)
                        {
                            Page++;
                            SearchByText(SearchText);
                        };
                        break;
                    case SearchFocus.Artists:
                        if (Page < artistPage.TotalPages)
                        {
                            Page++;
                            SearchByText(SearchText);
                        };
                        break;
                    case SearchFocus.Songs:
                        if (Page < songPage.TotalPages)
                        {
                            Page++;
                            SearchByText(SearchText);
                        };
                        break;
                }
            });



            PreviousResultPage = new RelayCommand(o=>Page>1,o => {
                Console.WriteLine("Prev result page - current page: " + Page);
                if (Page == 1) return;
                Page--;
                SearchByText(SearchText);
            });

            SetSearchFocus = new RelayCommand(o =>
            {
                SearchFocus sf = (SearchFocus)o;
                if (sf == SearchFocus)
                    SearchFocus = SearchFocus.Any;
                else
                    SearchFocus = sf;

                Page = 1;
                SearchByText(SearchText);
            });
        }

        private void SearchByText(string text)
        {
            Console.WriteLine("Searching for " + text);

            songPage = null;
            albumPage = null;
            artistPage = null;

            int amount = searchFocus == SearchFocus.Any ? 3 : 10;

            if (SearchFocus == SearchFocus.Any || SearchFocus == SearchFocus.Songs)
            {
                Response<Page<Song>> response = DataProvider.SearchSongsByTitle(text, Page, amount);
                if (!response.Status)
                {
                    ApplicationViewModel.HandleError(response.Error);
                    return;
                }
                songPage = response.Content;
            }
            if (SearchFocus == SearchFocus.Any || SearchFocus == SearchFocus.Albums)
            {
                Response<Page<Album>> response = DataProvider.SearchAlbumsByName(text, Page, amount);
                if (!response.Status)
                {
                    ApplicationViewModel.HandleError(response.Error);
                    return;
                }
                albumPage = response.Content;
            }
            if (SearchFocus == SearchFocus.Any || SearchFocus == SearchFocus.Artists)
            {
                Response<Page<Artist>> response = DataProvider.SearchArtistsByName(text, Page, amount);
                if (!response.Status)
                {
                    ApplicationViewModel.HandleError(response.Error);
                    return;
                }
                artistPage = response.Content;
            }

            SongResult = songPage != null ? new ObservableCollection<Song>(songPage.Items) : new ObservableCollection<Song>();
            AlbumResult = albumPage != null ? new ObservableCollection<Album>(albumPage.Items) : new ObservableCollection<Album>();
            ArtistResult = artistPage != null ? new ObservableCollection<Artist>(artistPage.Items) : new ObservableCollection<Artist>();
            

            /*
            Console.WriteLine(SongResult.Count());
            Console.WriteLine(AlbumResult.Count());
            Console.WriteLine(ArtistResult.Count());*/
        }




        private int page = 1;
        public int Page
        {
            get { return page; }
            set
            {
                page = value;
                OnPropertyChanged(nameof(Page));
            }
        }

        private SearchFocus searchFocus = SearchFocus.Any;
        public SearchFocus SearchFocus
        {
            get { return searchFocus; }
            set
            {
                searchFocus = value;
                OnPropertyChanged(nameof(SearchFocus));
            }
        }

        private ObservableCollection<Song> songResult;
        public ObservableCollection<Song> SongResult
        {
            get { return songResult; }
            set
            {
                songResult = value;
                OnPropertyChanged(nameof(SongResult));
            }
        }
        private ObservableCollection<Album> albumResult;
        public ObservableCollection<Album> AlbumResult
        {
            get { return albumResult; }
            set
            {
                albumResult = value;
                OnPropertyChanged(nameof(AlbumResult));
            }
        }
        private ObservableCollection<Artist> artistResult;
        public ObservableCollection<Artist> ArtistResult
        {
            get { return artistResult; }
            set
            {
                artistResult = value;
                OnPropertyChanged(nameof(ArtistResult));
            }
        }
    }
}
