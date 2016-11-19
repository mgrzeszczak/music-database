using Desktop.ViewModel;
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
using Common.Model;
using Desktop.Data;
using RestSharp;

namespace Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new ApplicationViewModel();
            InitializeComponent();
        }

        /*private IKernel SetUpNinject()
        {
            IKernel kernel = new StandardKernel();
            kernel.Bind(x => x.FromThisAssembly()
                    .IncludingNonePublicTypes()
                    .SelectAllClasses()
                    .Where(t => t.Name.Contains("Factory"))
                    .BindDefaultInterface()
                    .Configure((c, t) => c.InSingletonScope()));
            kernel.Bind(
                x =>x.FromThisAssembly()
                        .IncludingNonePublicTypes()
                        .SelectAllClasses()
                        .Where(t => !t.Name.Contains("Factory"))
                        .BindDefaultInterface()
                        .Configure(c => c.InTransientScope()));
            kernel.Bind<ApplicationViewModel>().ToSelf().InSingletonScope();
            return kernel;
        }*/
    }
}
