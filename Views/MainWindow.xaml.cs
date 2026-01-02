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
using Sampler.Services.Audio;
using Sampler.Services.AppConfiguration;

namespace Sampler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private  readonly   LogService       _logService;
        public MainWindow()
        {
            InitializeComponent();
            _logService = new LogService( LogBox );
            DataContext = new ViewModel ( _logService );
        }
    }
}
