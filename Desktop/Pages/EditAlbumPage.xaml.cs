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
    /// Interaction logic for EditAlbumPage.xaml
    /// </summary>
    public partial class EditAlbumPage : UserControl
    {
        public EditAlbumPage()
        {
            InitializeComponent();
            List<int> numbers = new List<int>();
            for (int i = 1; i <= 100; i++) numbers.Add(i);
            NumberComboBox.ItemsSource = numbers;
            numbers = new List<int>();
            for (int i = 1900; i <= DateTime.Now.Year; i++) numbers.Add(i);
            YearComboBox.ItemsSource = numbers;
        }
    }
}
