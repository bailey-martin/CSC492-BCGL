/*AppShell.xaml.cs
  Property of RAID Inc. (Andrew Moore, Bailey Martin, Kyle Hieb)
  University of Mount Union CSC 492
  Spring 2021 Semester
  Contact Information: raidincsoftware@gmail.com
  Class Description: This class is designed to support the AppShell.xaml class. It provides the global foundation for the entire application.
*/

using BCGL.Views;
using Xamarin.Forms;

namespace BCGL
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()    //defines navigation routes that are used to move been various UI pages throughout the application
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ListDetailedPage), typeof(ListDetailedPage));
            Routing.RegisterRoute(nameof(AddNewListPage), typeof(AddNewListPage));
            Routing.RegisterRoute(nameof(ListPage), typeof(ListPage));
            Routing.RegisterRoute(nameof(AddItemsToDBPage), typeof(AddItemsToDBPage));
            Routing.RegisterRoute(nameof(BarcodeScannerPage), typeof(BarcodeScannerPage));
            Routing.RegisterRoute(nameof(ProductDetailPage), typeof(ProductDetailPage));
        }

    }
}