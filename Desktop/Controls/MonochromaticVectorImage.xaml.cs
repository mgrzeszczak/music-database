using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Desktop.Controls
{
    /// <summary>
    /// Interaction logic for MonochromaticVectorImage.xaml
    /// </summary>
    public partial class MonochromaticVectorImage : UserControl
    {
        public static DependencyProperty ImageBrushProperty =
            DependencyProperty.Register(
                "ImageBrush",
                typeof(Brush),
                typeof(MonochromaticVectorImage));

        public Brush ImageBrush
        {
            get { return (Brush)GetValue(ImageBrushProperty); }
            set { SetValue(ImageBrushProperty, value); }
        }

        public MonochromaticVectorImage()
        {
            InitializeComponent();
        }
    }
}
