using System.Windows.Controls;
using System.Windows.Navigation;

namespace CsLaboratory2
{
    internal static class NavigationManager
    {
        public static NavigationWindow NavigationWindow { get; set; }

        internal static void Navigate(Page result)
        {
            NavigationWindow.NavigationService.Navigate(result);
        }
    }
}
