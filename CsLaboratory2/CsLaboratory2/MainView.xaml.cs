using System.Windows.Navigation;

namespace CsLaboratory2
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : NavigationWindow
    {
        public MainView()
        {
            InitializeComponent();
            StationManager.Initialize(new PersonCollection());
            NavigationManager.NavigationWindow = this;
        }
    }
}
