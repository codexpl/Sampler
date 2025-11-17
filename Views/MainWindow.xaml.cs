using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Sampler.ViewModels;
using Sampler.Helpers;

namespace Sampler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public    MenuViewModel     menuViewModel  { get; private set; }
        public    LogService        logService  {get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            logService = new LogService( LogBox );
            DataContext = new MenuViewModel ( logService );

        }


    }
}
