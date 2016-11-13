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

namespace Desktop.Pages
{
    /// <summary>
    /// Interaction logic for DisplaySongPage.xaml
    /// </summary>
    public partial class DisplaySongPage : UserControl
    {
        public DisplaySongPage()
        {
            InitializeComponent();
            //WebBrowser.Navigate("https://www.youtube.com/embed/9enOd3Z7vcY");
        }

        public void DisposeBrowser()
        {
            WebBrowser.Dispose();
        }

    }
}
