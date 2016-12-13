using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Desktop.Converters
{
    class TimeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TimeSpan length = (TimeSpan)value;
            var minutes = length.Hours*60 + length.Minutes;
            var secs = length.Seconds;
            return $"{(minutes<10? minutes.ToString("00") : minutes.ToString())}:{(secs <10? secs.ToString("00") : secs.ToString())}";
            //return length.ToString(@"hh\:mm\:ss");
            //return length.ToString(@"h\h\ m\ \m\i\n\ s\s");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
