using Common.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Desktop.Converters
{
    class GenreDisplayTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                
                var type = typeof(Genre);
                var name = Enum.GetName(type, value);
                FieldInfo fi = type.GetField(name);
                var descriptionAttrib = (DescriptionAttribute)
                    Attribute.GetCustomAttribute(fi, typeof(DescriptionAttribute));
                return descriptionAttrib.Description;
            } catch (Exception e)
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value as string;
            var values = Enum.GetValues(typeof(Genre));
            foreach(var val in values)
            {
                var name = Enum.GetName(typeof(Genre), val);
                FieldInfo fi = typeof(Genre).GetField(name);
                var descriptionAttrib = (DescriptionAttribute)
                    Attribute.GetCustomAttribute(fi, typeof(DescriptionAttribute));

                if (descriptionAttrib.Description == str) return val;
            }
            return Genre.ROCK;
        }
    }
}
