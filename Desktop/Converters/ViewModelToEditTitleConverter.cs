using Desktop.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Desktop.Converters
{
    class ViewModelToEditTitleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is EditSongViewModel) return "Edit Song";
            if (value is EditAlbumViewModel) return "Edit Album";
            if (value is EditArtistViewModel) return "Edit Arist";
            if (value is CreateSongViewModel) return "Add Song";
            if (value is CreateAlbumViewModel) return "Add Album";
            if (value is CreateArtistViewModel) return "Add Artist";
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
