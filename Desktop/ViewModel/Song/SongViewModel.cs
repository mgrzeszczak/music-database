using Common.Communication;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.ViewModel
{
    class SongViewModel : ChildViewModel
    {
        protected Song model;

        public SongViewModel(ApplicationViewModel applicationViewModel, IResponseDataProvider dataProvider) : base(applicationViewModel,dataProvider)
        {
            model = new Song();
        }

        public Song Model
        {
            get { return model; }
            set
            {
                model = value;
                OnPropertyChanged(nameof(Model));
            }
        }

        public string Title
        {
            get { return model.Title; }
            set
            {
                model.Title = value;
                OnPropertyChanged("Title");
            }
        }
        public Album Album
        {
            get { return model.Album; }
            set
            {
                model.Album = value;
                OnPropertyChanged("Album");
            }
        }
        public TimeSpan Length
        {
            get { return model.Length; }
            set
            {
                model.Length = value;
                OnPropertyChanged("Length");
            }
        }
        public int Number
        {
            get { return model.Number; }
            set
            {
                model.Number = value;
                OnPropertyChanged("Number");
            }
        }
        public string YoutubeUrl
        {
            get { return model.YoutubeUrl; }
            set
            {
                try
                {
                    string linkBase = "https://www.youtube.com/embed/";
                    int index = value.IndexOf("watch?v=");
                    if (index == -1) throw new ArgumentException();
                    index += "watch?v=".Length;
                    model.YoutubeUrl = linkBase + value.Substring(index);
                }
                catch (Exception)
                {
                    model.YoutubeUrl = null;
                }
                OnPropertyChanged("YoutubeUrl");
            }
        }
        public string Lyrics
        {
            get { return model.Lyrics; }
            set
            {
                model.Lyrics = value;
                OnPropertyChanged("Lyrics");
            }
        }

        protected override void InitializeCommands()
        {
            base.InitializeCommands();
        }
    }
}
