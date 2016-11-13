﻿using System;
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
    /// Interaction logic for EditSongPage.xaml
    /// </summary>
    public partial class EditSongPage : UserControl
    {
        public EditSongPage()
        {
            InitializeComponent();
            List<int> numbers = new List<int>();
            for (int i = 1; i <= 100; i++) numbers.Add(i);
            NumberComboBox.ItemsSource = numbers;
        }
    }
}
