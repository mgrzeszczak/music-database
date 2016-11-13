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
using System.Windows.Threading;

namespace Desktop.Controls
{

    public enum SearchModes
    {
        Instant,
        Delayed,
        Both
    }
    /// <summary>
    /// Interaction logic for SearchBar.xaml
    /// </summary>
    public partial class SearchBar : UserControl
    {
        #region Constructors
        public SearchBar()
        {
            //DataContext = this;
            InitializeComponent();
            SearchText = "";

            /*InstantSearch += (sender, args) =>
            {
                if (InstantSearchCommand != null && InstantSearchCommand.CanExecute(SearchText))
                    InstantSearchCommand.Execute(SearchText);
            };*/
            DelayedSearch += (sender, args) =>
            {
                if (DelayedSearchCommand != null && DelayedSearchCommand.CanExecute(SearchText))
                    DelayedSearchCommand.Execute(SearchText);
            };
            /*InstantSearch += (sender, args) =>
            {
                Console.WriteLine("doing instantsearch");
            };*/

            DelayedSearch += (sender, args) =>
            {
                Console.WriteLine("doing delayedsearch");
            };

            SetValue(ResultsSourceProperty, new List<FrameworkElement>());

            searchEventDelayTimer = new DispatcherTimer();
            searchEventDelayTimer.Interval = SearchEventTimeDelay.TimeSpan;
            searchEventDelayTimer.Tick += OnSearchEventDelayTimerTick;
        }

        #endregion // Constructors

        #region DependencyProperties

        public static DependencyProperty SearchTextProperty =
                DependencyProperty.Register(
                    "SearchText",
                    typeof(string),
                    typeof(SearchBar));

        public static DependencyProperty InstantSearchCommandProperty =
                DependencyProperty.Register(
                    "InstantSearchCommand",
                    typeof(ICommand),
                    typeof(SearchBar));

        public static DependencyProperty DelayedSearchCommandProperty =
                DependencyProperty.Register(
                    "DelayedSearchCommand",
                    typeof(ICommand),
                    typeof(SearchBar));

        public static DependencyProperty ResultsSourceProperty =
                DependencyProperty.Register(
                    "ResultsSource",
                    typeof(IList<FrameworkElement>),
                    typeof(SearchBar),
                    new FrameworkPropertyMetadata(
                        new List<FrameworkElement>()));

        public static DependencyProperty SearchEventTimeDelayProperty =
                DependencyProperty.Register(
                "SearchEventTimeDelay",
                typeof(Duration),
                typeof(SearchBar),
                new FrameworkPropertyMetadata(
                    new Duration(new TimeSpan(0, 0, 0, 0, 500)),
                    new PropertyChangedCallback(OnSearchEventTimeDelayChanged)));

        public static DependencyProperty SearchModeProperty =
                DependencyProperty.Register(
                    "SearchMode",
                    typeof(SearchModes),
                    typeof(SearchBar),
                    new PropertyMetadata(SearchModes.Both));

        public static readonly RoutedEvent InstantSearchEvent =
            EventManager.RegisterRoutedEvent(
                "InstantSearch",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(SearchBar));

        public static readonly RoutedEvent DelayedSearchEvent =
            EventManager.RegisterRoutedEvent(
                "DelayedSearch",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(SearchBar));

        #endregion // DependencyProperties

        #region Properties

        public string SearchText
        {
            get { return GetValue(SearchTextProperty) as string; }
            set { SetValue(SearchTextProperty, value); }
        }

        public ICommand InstantSearchCommand
        {
            get { return GetValue(InstantSearchCommandProperty) as ICommand; }
            set { SetValue(InstantSearchCommandProperty, value); }
        }

        public ICommand DelayedSearchCommand
        {
            get { return GetValue(DelayedSearchCommandProperty) as ICommand; }
            set { SetValue(DelayedSearchCommandProperty, value); }
        }

        public SearchModes SearchMode
        {
            get { return (SearchModes)GetValue(SearchModeProperty); }
            set { SetValue(SearchModeProperty, value); }
        }

        public IList<FrameworkElement> ResultsSource
        {
            get { return GetValue(ResultsSourceProperty) as IList<FrameworkElement>; }
            set { SetValue(ResultsSourceProperty, value); }
        }

        public Duration SearchEventTimeDelay
        {
            get { return (Duration)GetValue(SearchEventTimeDelayProperty); }
            set { SetValue(SearchEventTimeDelayProperty, value); }
        }

        #endregion // Properties

        #region Events

        public event RoutedEventHandler InstantSearch
        {
            add { AddHandler(InstantSearchEvent, value); }
            remove { RemoveHandler(InstantSearchEvent, value); }
        }

        public event RoutedEventHandler DelayedSearch
        {
            add { AddHandler(DelayedSearchEvent, value); }
            remove { RemoveHandler(DelayedSearchEvent, value); }
        }

        private void RaiseInstantSearchEvent()
        {
            RoutedEventArgs args = new RoutedEventArgs(InstantSearchEvent);
            RaiseEvent(args);
        }

        private void RaiseDelayedSearchEvent()
        {
            RoutedEventArgs args = new RoutedEventArgs(DelayedSearchEvent);
            RaiseEvent(args);
        }

        #endregion // Events

        private DispatcherTimer searchEventDelayTimer;

        static void OnSearchEventTimeDelayChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            SearchBar searchBar = o as SearchBar;
            if (searchBar != null)
            {
                searchBar.searchEventDelayTimer.Interval = ((Duration)e.NewValue).TimeSpan;
                searchBar.searchEventDelayTimer.Stop();
            }
        }

        void OnSearchEventDelayTimerTick(object o, EventArgs e)
        {
            searchEventDelayTimer.Stop();
            RaiseInstantSearchEvent();
        }

        private void SearchText_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (string.IsNullOrWhiteSpace(SearchText))
                    return;
                if (searchEventDelayTimer.IsEnabled)
                    searchEventDelayTimer.Stop();
                RaiseDelayedSearchEvent();
                return;
            }
            if (e.Key == Key.Escape)
            {
                SearchText = "";
                return;
            }
        }

        private void SearchText_OnLostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void SearchTB_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchMode == SearchModes.Instant || SearchMode == SearchModes.Both && !string.IsNullOrWhiteSpace(SearchText))
            {
                searchEventDelayTimer.Stop();
                searchEventDelayTimer.Start();
            }
        }
    }
}
