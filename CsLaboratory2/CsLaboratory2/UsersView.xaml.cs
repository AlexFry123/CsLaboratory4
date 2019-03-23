using System.Windows.Controls;

namespace CsLaboratory2
{
    /// <summary>
    /// Interaction logic for UsersView.xaml
    /// </summary>
    public partial class UsersView : Page
    {
        public UsersView()
        {
            InitializeComponent();
            DataContext = new UserListViewModel();
        }
    }
}
